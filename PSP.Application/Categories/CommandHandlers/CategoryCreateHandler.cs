using MediatR;
using PSP.Application.Categories.Commands;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.CategoryAggregate;
using PSP.Domain.Exceptions;

namespace PSP.Application.Categories.CommandHandlers; 

public class CategoryCreateHandler : IRequestHandler<CategoryCreate, OperationResult<Category>>
{
    private readonly DataContext _ctx;

    public CategoryCreateHandler(DataContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OperationResult<Category>> Handle(CategoryCreate request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Category>();

        try
        {
            var category = Category.CreateCategory(request.CategoryName, request.CategoryDescription);
            _ctx.Categories.Add(category);
            await _ctx.SaveChangesAsync();
            result.Payload = category;
        }
        catch (CategoryNotValidException ex)
        {
            ex.ValidationErrors.ForEach(e =>
            {
                result.AddError(ErrorCode.ValidationError, e);
            });
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}