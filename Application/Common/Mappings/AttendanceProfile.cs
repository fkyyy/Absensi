using System;
using Application.Common.Dtos.Attendances;
using AutoMapper;
using Domain;

namespace Application.Common.Mappings;


public class AttendanceProfile : Profile
{
    public AttendanceProfile()
    {
        CreateMap<Attendance, AttendanceDto>();
        CreateMap<CreateAttendanceDto, Attendance>();
        CreateMap<AbsenDto, Attendance>();
        CreateMap<UpdateAttendanceDto , Attendance>();
    }
}

