using Microsoft.AspNetCore.Authorization;
using PSP.Api.Contracts.Category.Request;
using PSP.Application.Categories.Commands;
using PSP.Application.Categories.Queries;

namespace PSP.Api.Controllers; 

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class CategoryController : BaseController{
    
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategory newCategory) {
        var command = new CategoryCreate {
            CategoryName = newCategory.CategoryName,
            CategoryDescription = newCategory.CategoryDescription
        };

        var result = await _mediator.Send(command);
        return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories() {
        var result = await _mediator.Send(new GetAllCategories());
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet]
    [Route(ApiRoutes.Categories.IdRoute)]
    [ValidateGuid("id")]
    public async Task<IActionResult> GetCategoryById(string id) {
        var categoryId = Guid.Parse(id);
        var query = new GetCategoryById { CategoryId = categoryId };
        var result = await _mediator.Send(query);

        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

}