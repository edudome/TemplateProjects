using AutoMapper;
using Template.Domain.Entities;
using Template.Domain.InterfacesRepository;
using Template.Infrastructure.Context;
using Template.Infrastructure.Generics;

namespace Template.Infrastructure.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario> , IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}
