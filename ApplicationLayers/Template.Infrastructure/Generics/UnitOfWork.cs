using AutoMapper;
using Infrastructure.Repository;
using Microsoft.Extensions.Options;
using Template.Domain.AppSettings;
using Template.Domain.Core;
using Template.Domain.InterfacesRepository;
using Template.Infrastructure.Context;
using Template.Infrastructure.Repository;

namespace Template.Infrastructure.Generics
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConfigurationSettings _config;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        private SqlDataAccess? _sqlDataAccess;

        private UsuarioRepository? _usuarioRepository;

        private AAARepository? _aaaRepository;

        public UnitOfWork(IOptions<ConfigurationSettings> config, IMapper mapper, ApplicationDbContext context)
        {
            _config = config.Value;
            _mapper = mapper;
            _context = context;
        }

        public ISqlDataAccess SqlDataAccess => _sqlDataAccess ??= new SqlDataAccess(_config, _mapper);

        public IUsuarioRepository Usuarios => _usuarioRepository ??= new UsuarioRepository(_context, _mapper);

        public IAAARepository AAAs => _aaaRepository ??= new AAARepository(_context, _mapper);

    }
}
