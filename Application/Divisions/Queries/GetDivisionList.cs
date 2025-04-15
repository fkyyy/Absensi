using Application.Common.Dtos.Divisions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using AutoMapper.QueryableExtensions;

namespace Application.Divisions.Queries;

public class GetDivisionList
{
    public class Query : IRequest<List<DivisionDto>> { }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, List<DivisionDto>>
    {
        public async Task<List<DivisionDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Divisions
                .ProjectTo<DivisionDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
