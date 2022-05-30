using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.CategoryAggregate;

namespace PSP.Application.Categories.Commands; 

public class CategoryCreate : IRequest<OperationResult<Category>>
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
}