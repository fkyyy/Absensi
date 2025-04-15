using System;
using Application.Common.Dtos.Divisions;
using MediatR;
using Persistence;

namespace Application.Divisions.Commands;

public class  EditDivision
{
    public class Command : IRequest
    {
        public required DivisionDto Division { get; set; }

    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
    
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
           var userEntity = await context.Divisions
                .FindAsync([request.Division.IdDivision], cancellationToken)
                    ?? throw new Exception("division not found");
            userEntity.DivisionName = request.Division.DivisionName;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
