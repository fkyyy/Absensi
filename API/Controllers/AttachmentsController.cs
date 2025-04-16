using System;
using System.Security.Claims;
using Application.Attachments.Commands;
using Application.Attachments.Queries;
using Application.Attendances.Commands;
using Application.Common.Dtos.Attachments;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class AttachmentsController : BaseAPIController
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
    public async Task<ActionResult<string>> CreateAttachments([FromForm] CreateAttachmentDto at)
    {
        if (at == null || at.FileSource == null || at.FileSource.Length == 0)
            return BadRequest("Attachment data or file is invalid.");

        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(at.FileSource.FileName)}";
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        var savePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(savePath, FileMode.Create))
        {
            await at.FileSource.CopyToAsync(stream);
        }
        
        var updatedFormFile = new FormFile(at.FileSource.OpenReadStream(), 0, at.FileSource.Length, at.FileSource.Name, fileName)
        {
            Headers = at.FileSource.Headers,
            ContentType = at.FileSource.ContentType
        };

        at.FileSource = updatedFormFile;

        
        var result = await mediator.Send(new CreateAttachment.Command { Attachment = at });

        if (string.IsNullOrEmpty(result))
            return BadRequest("Failed to create attachment.");

        return CreatedAtAction(nameof(GetAttachmentDetails), new { AttachmentId = result }, result);
    }

    [Authorize]
    [HttpGet("download/{attachmentId}")]
    public async Task<IActionResult> Download(Guid attachmentId)
    {
        var result = await mediator.Send(new DownloadAttachment.Query { AttachmentId = attachmentId });

        return File(result.FileBytes, result.ContentType, result.FileName);
    }
}
