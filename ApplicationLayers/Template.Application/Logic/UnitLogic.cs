using AutoMapper;
using Template.Domain.Core;
using Template.Application.Logic.Account;
using Template.Application.Logic.AAAs.Empty;
using Template.Application.Logic.AAAs;
using Template.Application.Logic.Importacion.AAAs;

namespace Template.Application.Logic
{
    public interface IUnitLogic
    {
        AccountLogic AccountLogic { get; }


        EmptyLogic EmptyLogic { get; }

        AAALogic AAALogic { get; }
        ImportacionAAALogic ImportacionAAALogic { get; }
    }

    public class UnitLogic : IUnitLogic
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private AccountLogic? _accountLogic;


        private EmptyLogic? _emptyLogic;
        private AAALogic? _aaaLogic;
        private ImportacionAAALogic? _importacionAAALogic;

        public UnitLogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public AccountLogic AccountLogic => _accountLogic ??= new AccountLogic(_mapper, _unitOfWork);

        public EmptyLogic EmptyLogic => _emptyLogic ??= new EmptyLogic(_mapper, _unitOfWork);
        public AAALogic AAALogic => _aaaLogic ??= new AAALogic(_mapper, _unitOfWork);
        public ImportacionAAALogic ImportacionAAALogic => _importacionAAALogic ??= new ImportacionAAALogic(_mapper, _unitOfWork);

    }
}
