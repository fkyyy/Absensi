using Application.Common.Dtos.Divisions;
using AutoMapper;
using Domain;

public class DivisionProfile : Profile
{
    public DivisionProfile()
    {
        CreateMap<Division, DivisionDto>();
        CreateMap<CreateDivisionDto, Division>();
    }
}
