using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Template.Domain.AppSettings;
using Template.Domain.Core;
using Template.Domain.Exceptions;
using Template.Infrastructure.FakeSPs;

namespace Template.Infrastructure.Generics
{

    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly ConfigurationSettings _config;
        private readonly IMapper _mapper;

        public SqlDataAccess(ConfigurationSettings config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        /// <summary>
        /// Arma el modelo con lo devuelto de la consulta
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T?> Execute<T>(string query, object? parameters = null, int commandTimeout = 30)
        {
            try
            {
                var res = await Command<T>(query, parameters, commandTimeout);
                T? resultado = _mapper.Map<T?>(res);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Genera la consulta en la base de datos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Es Case Sensitive para los nombres de los parámetros.</param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        /// <exception cref="DataBaseException"></exception>
        private async Task<object?> Command<T>(string query, object? parameters = null, int commandTimeout = 30)
        {
            try
            {
                string? connectionString = _config.ConnectionStrings?.SqlDB;
                bool esUnitTest = _config.EsUnitTest;
                bool esLista = false;
                bool esIEnumerable = false;
                bool esInteger = false;

                try
                {
                    Type listType = typeof(T);
                    esInteger = (listType == typeof(int));
                    esLista = (typeof(T).GetGenericTypeDefinition() == typeof(List<>));
                    esIEnumerable = (typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>));
                }
                catch
                {

                }

                CommandType commandType = (query.ToUpper().StartsWith("DBO.")) ? CommandType.StoredProcedure : CommandType.Text;
                IDbConnection? connection = null;

                if (esUnitTest)
                {
                    connection = new SqliteConnection(connectionString);
                    if (commandType == CommandType.StoredProcedure)
                    {
                        var result = await FakeSP.GetFakeResultFromSP(query.ToUpper());
                        return result;
                    }
                }
                else
                {
                    connection = new SqlConnection(connectionString);
                }

                if (connection.State != ConnectionState.Open) connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (esLista || esIEnumerable)
                        {
                            var resultList = await connection.QueryAsync(query, parameters, transaction, commandTimeout, commandType);
                            transaction.Commit();
                            if (!esUnitTest) connection.Close();
                            return resultList;
                        }
                        
                        if (esInteger)
                        {
                            var resultData = await connection.ExecuteScalarAsync(query, parameters, transaction, commandTimeout, commandType);
                            transaction.Commit();
                            if (!esUnitTest) connection.Close();
                            return resultData;
                        }

                        var resultModel = await connection.QueryFirstOrDefaultAsync(query, parameters, transaction, commandTimeout, commandType);
                        transaction.Commit();
                        if (!esUnitTest) connection.Close();
                        return resultModel;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        if (!esUnitTest) connection.Close();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
    }
}
