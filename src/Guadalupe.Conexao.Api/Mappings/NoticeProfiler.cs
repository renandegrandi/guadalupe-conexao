using AutoMapper;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Models.V1;
using Dto = Guadalupe.Conexao.Api.Models.V1;

namespace Guadalupe.Conexao.Api.Mappings
{
    public class NoticeProfiler : Profile
    {
        public NoticeProfiler()
        {
            CreateMap<Domain.Notice, Dto.NoticeDto>()
                .ForMember((dto) => dto.Posted, opt => opt.MapFrom((domain) => domain.Registration))
                .ForMember((dto) => dto.State, opt => opt.MapFrom<StateResolver>());
        }
    }

    class StateResolver : IValueResolver<Domain.Notice, Dto.NoticeDto, UserNoticeState>
    {
        public UserNoticeState Resolve(Notice source, NoticeDto destination, UserNoticeState destMember, ResolutionContext context)
        {
            var dateModified = source.Modification;
            var dateRemoved = source.Removal;

            if (dateRemoved.HasValue)
                return UserNoticeState.Removed;

            if (dateModified.HasValue)
                return UserNoticeState.Modified;

            return UserNoticeState.Included;

        }
    }
}
