using Application.Common.Dtos.Divisions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Divisions.Queries;

public class GetDivisionById
{
    public class Query : IRequest<DivisionDto>
    {
        public Guid IdDivision { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, DivisionDto>
    {
        public async Task<DivisionDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var division = await context.Divisions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdDivision == request.IdDivision, cancellationToken);

            if (division == null)
                throw new Exception("Division not found");

            return mapper.Map<DivisionDto>(division);
        }
    }
}
