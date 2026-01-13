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
        CreateMap<OptionEntity, OptionResultsDto>();
        // CreateMap<PollEntity, PollResultsDto>();
    }
}