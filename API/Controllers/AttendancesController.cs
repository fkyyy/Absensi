using System;
using System.Security.Claims;
using Application.Attendances.Commands;
using Application.Attendances.Queries;
using Application.Common.Dtos.Attendances;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers;

public class AttendancesController : BaseAPIController
{
   [HttpGet]
    public async Task<ActionResult<List<AttendanceDto>>> GetAttendances()
    {
        var attendances = await mediator.Send(new GetAttendancesList.Query());
        if (attendances == null || attendances.Count == 0)
            return NotFound("No Attendances found.");
        return Ok(attendances);
    }

    [HttpGet("{IdAttendance}")]
    public async Task<ActionResult<AttendanceDto>> GetAttendanceDetails(Guid IdAttendance)
    {
       var att = await mediator.Send(new GetAttendancesById.Query { IdAttendance = IdAttendance });
        if (att == null)
            return NotFound($"attendance with ID {IdAttendance} not found.");
        return Ok(att);
    }

    [Authorize]
    [HttpPost("checkin")]
    public async Task<ActionResult<string>> CheckIn([FromBody] CreateAttendanceDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (dto.AttendanceType == EnumType.Izin || dto.AttendanceType == EnumType.Sakit)
            return BadRequest("Gunakan endpoint /absen untuk izin/sakit.");

        dto.IdUser = Guid.Parse(userId);

        var result = await mediator.Send(new CreateAttendance.Command
        {
            Attendance = dto
        });

        return Ok(new { message = "Check-in berhasil", IdAttendance = result });
    }


    [Authorize]
    [HttpPost("checkout")]
    public async Task<ActionResult> CheckOut()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        await mediator.Send(new UpdateAttendance.Command
        {
            IdUser = Guid.Parse(userId)
        });

        return Ok(new { message = "Check-out successful" });
    }

    [Authorize]
    [HttpPost("absen")]
    public async Task<IActionResult> CreateAbsen([FromBody] AbsenDto dto)
    {
        if (dto == null)
            return BadRequest("Data absen tidak valid.");

        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr == null)
            return Unauthorized("User ID tidak ditemukan.");

        var userId = Guid.Parse(userIdStr);

        if (dto.AttendanceType != EnumType.Izin && dto.AttendanceType != EnumType.Sakit)
            return BadRequest("Gunakan endpoint /checkin untuk absensi hadir (WFO/WFH).");

        dto.IdUser = userId;

        if (dto.IsApproved?.ToLower() == "Approved")
        {
            dto.Status = "Complete";
        }
        else if (dto.IsApproved?.ToLower() == "Rejected")
        {
            dto.Status = "Failed";
        }
        else
        {
            dto.Status = "Pending";
        }

        var result = await mediator.Send(new CreateAbsen.Command { Attendance = dto });

        return Ok(new
        {
            message = "Pengajuan absensi izin/sakit berhasil",
            status = dto.Status,
            IdAttendance = result
        });
    }




    [Authorize(Roles = "Leader,Admin")]
    [HttpPut("approve-absen/{id}")]
    public async Task<IActionResult> ApproveIzinSakit(Guid id, [FromBody] ApproveAbsenDto request)
    {
        if (request.Status != "Approved" && request.Status != "Rejected")
        return BadRequest("Status approval harus 'Approved' atau 'Rejected'.");

        await mediator.Send(new ApproveAbsen.Command
        {
            IdAttendance = id,
            IsApproved = request.Status
        });

        return Ok(new { message = $"Absensi berhasil di-{request.Status.ToLower()}" });
    }

}
