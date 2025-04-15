using System;
using Domain;
using Application.Common.Dtos.Attachments;
using AutoMapper;

namespace Application.Common.Mappings;

public class AttachmentProfile : Profile
{
    public AttachmentProfile()
    {
        CreateMap<Attachment, AttachmentDto>();
        CreateMap<CreateAttachmentDto, Attachment>();
    }
}
