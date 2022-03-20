using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Template.Application.Handlers.AAAs;
using Template.Application.Models.AAAs;
using Template.Domain.Core;
using Template.Domain.Entities;
using Template.Domain.Exceptions;

namespace Template.Application.Logic.AAAs
{
    public class AAALogic
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AAALogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /////////////////////// CRUD ///////////////////

        public async Task<int> Add(AddAAA request)
        {
            try
            {
                AAA db = _mapper.Map<AAA>(request);
                return await _unitOfWork.AAAs.AddAsync(db);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddOrUpdate(AddAAA request)
        {
            try
            {
                AAA db = _mapper.Map<AAA>(request);
                return await _unitOfWork.AAAs.AddOrUpdateAsync(db, e => e.Cedula != null && e.Cedula.Equals(db.Cedula));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AAAModel?> GetById(int id)
        {
            try
            {
                // Con EntityFramework
                //AAA? db = await _unitOfWork.AAAs.GetByIdAsync(id);
                //AAAModel? result = _mapper.Map<AAAModel?>(db);

                // Con Dapper
                //AAAModel? result = await _unitOfWork.SqlDataAccess.Execute<AAAModel>("select * from AAA where AAAId = @id", new { id });
                AAAModel? result = await _unitOfWork.SqlDataAccess.Execute<AAAModel>("dbo.GET_AAA_BY_ID", new { AAAId = id });
                
                return await Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AAAModel>?> GetAll()
        {
            try
            {
                // Con Entity Framework
                //List<AAA>? db = await _unitOfWork.AAAs.GetAllAsync();
                //List<AAAModel>? result = _mapper.Map<List<AAAModel>?>(db);

                // Con Dapper
                List<AAAModel>? result = await _unitOfWork.SqlDataAccess.Execute<List<AAAModel>>("dbo.GET_AAAS");
                //List<AAAModel>? result = await _unitOfWork.SqlDataAccess.Execute<List<AAAModel>>("select * from AAA");

                return await Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateAAA request)
        {
            try
            {
                // Con EntityFramework
                AAA? db = _mapper.Map<AAA?>(request);
                await _unitOfWork.AAAs.UpdateAsync(db);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteById(int id)
        {
            try
            {
                // Con EntityFramework
                //await _unitOfWork.AAAs.DeleteByIdAsync(id);

                // Con Dapper
                int result = await _unitOfWork.SqlDataAccess.Execute<int>("dbo.DELETE_AAA_BY_ID", new { AAAId = id });

                switch (result)
                {
                    case 0:
                        throw new DataBaseException("Los AAAs ya están importados.");
                    case -1:
                        throw new DataBaseException("No se encontró el área de los AAAs.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////////////////////////////////////////////////

        public async Task DeleteByEmpresaId(int empresaId)
        {
            try
            {
                await _unitOfWork.AAAs.DeleteWhereAsync(e => 1 == 1);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
