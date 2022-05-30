using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.CategoryAggregate;

namespace PSP.Application.Categories.Queries; 

public class GetCategoryById : IRequest<OperationResult<Category>>{
    public Guid CategoryId { get; set; }
}