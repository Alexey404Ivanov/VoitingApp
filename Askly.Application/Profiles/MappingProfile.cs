using AutoMapper;
using Askly.Domain.Entities;
using Askly.Application.DTOs;
namespace Askly.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PollEntity, PollDto>();
        CreateMap<CreatePollDto, PollEntity>();
        CreateMap<CreateOptionDto, OptionEntity>();
        CreateMap<OptionEntity, OptionDto>();
        CreateMap<OptionEntity, VoteResultsDto>();
        CreateMap<Tuple<Guid, int>, VoteResultsDto>()
            .ForMember(
                d => d.OptionId,
                opt => opt.MapFrom(s => s.Item1))
            .ForMember(
                d => d.VotesCount,
                opt => opt.MapFrom(s => s.Item2));

        // CreateMap<PollEntity, PollResultsDto>();
    }
}