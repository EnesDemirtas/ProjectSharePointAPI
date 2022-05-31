using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Categories.Queries;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects;
using PSP.Dal;
using PSP.Domain.Aggregates.CategoryAggregate;

namespace PSP.Application.Categories.QueryHandlers; 

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, OperationResult<Category>>
{
    private readonly DataContext _ctx;

    public GetCategoryByIdHandler(DataContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OperationResult<Category>> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Category>();
        var post = await _ctx.Categories.FirstOrDefaultAsync(p => p.CategoryId == request.CategoryId);

        if (post is null)
        {
            result.AddError(ErrorCode.NotFound, 
                string.Format(CategoryErrorMessages.CategoryNotFound, request.CategoryId));
            return result;
        }

        result.Payload = post;
        return result;
    }
}