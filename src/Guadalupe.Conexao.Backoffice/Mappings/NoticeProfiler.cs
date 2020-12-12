using AutoMapper;
using Dto = Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using ViewModel = Guadalupe.Conexao.Backoffice.Models;

namespace Guadalupe.Conexao.Backoffice.Mappings
{
    sealed class NoticeProfiler : Profile
    {
        public NoticeProfiler()
        {
            CreateMap<ViewModel.NoticeViewModel, Dto.NoticeDto>()
                .ReverseMap();

            CreateMap<ViewModel.NoticeViewModel, Dto.NoticeCreateDto>();
        }
    }
}
