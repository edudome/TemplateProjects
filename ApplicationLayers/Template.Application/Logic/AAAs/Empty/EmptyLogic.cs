using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Template.Domain.Core;
using Template.Application.Handlers.AAAs.Empty;

namespace Template.Application.Logic.AAAs.Empty
{
    public class EmptyLogic
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmptyLogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Do(XCommand request)
        {
            try
            {
                //EntityX db = _mapper.Map<EntityX>(request);
                //return await _unitOfWork.XXX.AddAsync(db);

                return await Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
