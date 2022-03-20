using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Template.Application.Handlers.Importacion.AAAs;
using Template.Application.Utils;
using Template.Domain.Core;
using Template.Domain.Exceptions;

namespace Template.Application.Logic.Importacion.AAAs
{
    public class ImportacionAAALogic
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ImportacionAAALogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        ////////////////////////////////////////////////

        public async Task<bool> Importar(ImportacionAAA request)
        {
            try
            {
                var parametros = new { AAAs = request.AAAsList?.ToDataTable(), borrarPrevios = request.BorrarPrevios };
                int result = await _unitOfWork.SqlDataAccess.Execute<int>("dbo.IMPORTACION_AAAS", parametros);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////////////////////////////////////////////////


    }
}
