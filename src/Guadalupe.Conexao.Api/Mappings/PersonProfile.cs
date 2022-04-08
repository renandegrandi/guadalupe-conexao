using AutoMapper;
using Dto = Guadalupe.Conexao.Api.Models.V1;

namespace Guadalupe.Conexao.Api.Mappings
{
    sealed class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Domain.Person, Dto.PersonDto>();
        }
    }
}
