using System;
using System.Security.Claims;
using Application.Attachments.Commands;
using Application.Attachments.Queries;
using Application.Attendances.Commands;
using Application.Common.Dtos.Attachments;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class AttachmentsController(Cloudinary cloudinary) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<AttachmentDto>>> GetAttachments()
    {
        var attach = await mediator.Send(new GetAttachmentList.Query());
        if (attach == null || attach.Count == 0)
            return NotFound("No Attachment found.");
        return Ok(attach);
    }

    [HttpGet("{AttachmentId}")]
    public async Task<ActionResult<AttachmentDto>> GetAttachmentDetails(Guid AttachmentId)
    {
       var attac = await mediator.Send(new GetAttachmentById.Query { AttachmentId = AttachmentId });
        if (attac == null)
            return NotFound($"attendance with ID {AttachmentId} not found.");
        return Ok(attac);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File tidak valid.");

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = "absensi_files"
        };

        var result = await cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            return StatusCode(500, "Upload gagal.");

        var attachment = new CreateAttachmentDto
        {
            FileSource = file,
            FileType = result.Format,
            FileUrl = result.Url.ToString()
        };

        var savedId = await mediator.Send(new CreateAttachment.Command { Attachment = attachment });

        return CreatedAtAction(nameof(GetAttachmentDetails), new { AttachmentId = savedId }, attachment);
    }



    [Authorize]
    [HttpGet("download/{attachmentId}")]
    public async Task<IActionResult> Download(Guid attachmentId)
    {
        var result = await mediator.Send(new DownloadAttachment.Query { AttachmentId = attachmentId });

        return File(result.FileBytes, result.ContentType, result.FileName);
    }
}
