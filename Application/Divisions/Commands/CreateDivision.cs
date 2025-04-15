using System;
using Application.Common.Dtos.Divisions;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Divisions.Commands;

public class CreateDivision
{
    public class Command : IRequest<string>
    {
        public required CreateDivisionDto Division { get; set; }
    }
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
             var divisionEntity = mapper.Map<Division>(request.Division);

            context.Divisions.Add(divisionEntity);
            await context.SaveChangesAsync(cancellationToken);

            return divisionEntity.IdDivision.ToString();
        }
    }
}
