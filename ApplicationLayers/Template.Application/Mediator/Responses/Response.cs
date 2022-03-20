using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Exceptions;

namespace Template.Application.Mediator.Responses
{
    public static class Response
    {
        public static Response<T?> Ok<T>(T? data, string message = "") => new(data, message, false);

        public static Response<T?> Fail<T>(int errorCode, string message) => new (errorCode, message, true, 0, 0);

        public static Response<T?> Fail<T>(Exception exception)
        {
            string message = exception.Message;
            if (exception is NoDataException) return new(200, message, true, 0, 0);
            if (exception is DataBaseException)
            {
                int errorCodigoSubTipo = 0;
                int errorLinea = 0;

                if (message.StartsWith("###sp_error###")) // Es error de SP
                {
                    string[] splitMsg = message.Split(new string[] { "###sp_error###" }, StringSplitOptions.RemoveEmptyEntries);
                    errorCodigoSubTipo = int.Parse(splitMsg[0]);
                    errorLinea = int.Parse(splitMsg[1]);
                    message = splitMsg[2];
                }
                return new(300, message, true, errorCodigoSubTipo, errorLinea);
            }
            if (exception is UnAuthorizedException) return new(401, message, true, 0, 0);
            return new(500, message, true, 0, 0);
        }
    }

    public interface IResponse
    {
        string Mensaje { get; set; }
        bool HayError { get; set; }
        public int ErrorCodigo { get; set; }
    }


    public class Response<T> : IResponse
    {
        public Response(T? data, string msg, bool error)
        {
            Data = data;
            Mensaje = msg;
            HayError = error;
        }
        public Response(int errorCodigo, string msg, bool error, int errorCodigoSubTipo, int errorLinea)
        {
            Data = default(T);
            Mensaje = msg;
            HayError = error;
            ErrorCodigo = errorCodigo;
            ErrorCodigoSubTipo = errorCodigoSubTipo;
            ErrorLinea = errorLinea;
        }

        public T? Data { get; set; }
        public string Mensaje { get; set; }
        public bool HayError { get; set; }
        public int ErrorCodigo { get; set; }

        /// Para Stored Procedures ///
        /// <summary>
        /// El código de error indicado en el mensaje del Throw en el SP
        /// </summary>
        public int ErrorCodigoSubTipo { get; set; }

        public int ErrorLinea { get; set; }

    }
}
