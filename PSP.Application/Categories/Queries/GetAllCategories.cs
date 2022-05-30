using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.CategoryAggregate;

namespace PSP.Application.Categories.Queries; 

public class GetAllCategories : IRequest<OperationResult<List<Category>>> {
    
}