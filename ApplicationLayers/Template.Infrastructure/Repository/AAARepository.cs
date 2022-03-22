using AutoMapper;
using Template.Domain.Entities;
using Template.Domain.InterfacesRepository;
using Template.Infrastructure.Context;
using Template.Infrastructure.Generics;

namespace Template.Infrastructure.Repository
{
    public class AAARepository : BaseRepository<AAA>, IAAARepository
    {
        public AAARepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}
