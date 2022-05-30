using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Categories.Queries;
using PSP.Application.Models;
using PSP.Application.Projects.Queries;
using PSP.Dal;
using PSP.Domain.Aggregates.CategoryAggregate;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Categories.QueryHandlers; 

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, OperationResult<List<Category>>>
{
    private readonly DataContext _ctx;

    public GetAllCategoriesHandler(DataContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OperationResult<List<Category>>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<Category>>();

        try
        {
            var categories = await _ctx.Categories.ToListAsync(cancellationToken);
            result.Payload = categories;
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);

        }

        return result;
    }
}