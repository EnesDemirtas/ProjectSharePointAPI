namespace PSP.Api.Controllers {

    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class ProjectsController : BaseController {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectsController(IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects() {
            var result = await _mediator.Send(new GetAllProjects());
            var mapped = _mapper.Map<List<ProjectResponse>>(result.Payload);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetProjectById(string id) {
            var projectId = Guid.Parse(id);
            var query = new GetProjectById { ProjectId = projectId };
            var result = await _mediator.Send(query);

            var mapped = _mapper.Map<ProjectResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreate newPost) {
            var command = new CreateProject() {
                UserProfileId = Guid.Parse(newPost.UserProfileId),
                TextContent = newPost.TextContent
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<ProjectResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) :
                CreatedAtAction(nameof(GetProjectById), new { id = result.Payload.UserProfileId }, mapped);
        }

        [HttpPatch]
        [Route(ApiRoutes.Projects.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProjectText([FromBody] ProjectUpdate updatedPost, string id) {
            var command = new UpdateProjectContent() {
                NewText = updatedPost.Text,
                ProjectId = Guid.Parse(id)
            };

            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeleteProject(string id) {
            var command = new DeleteProject() { ProjectId = Guid.Parse(id) };
            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.ProjectComments)]
        [ValidateGuid("projectId")]
        public async Task<IActionResult> GetCommentsByProjectId(string projectId) {
            var query = new GetProjectComments() { ProjectId = Guid.Parse(projectId) };
            var result = await _mediator.Send(query);

            if (result.IsError) HandleErrorResponse(result.Errors);

            var comments = _mapper.Map<List<ProjectCommentResponse>>(result.Payload);
            return Ok(comments);
        }

        [HttpPost]
        [Route(ApiRoutes.Projects.ProjectComments)]
        [ValidateGuid("projectId")]
        [ValidateModel]
        public async Task<IActionResult> AddCommentToProject(string projectId, [FromBody] ProjectCommentCreate comment) {
            var isValidGuid = Guid.TryParse(comment.UserProfileId, out var userProfileId);
            if (!isValidGuid) {
                var apiError = new ErrorResponse();
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad Request";
                apiError.Timestamp = DateTime.Now;
                apiError.Errors.Add("Provided User Profile ID is not a valid Guid format.");

                return BadRequest(apiError);
            }
            var command = new AddProjectComment() {
                ProjectId = Guid.Parse(projectId),
                UserProfileId = userProfileId,
                CommentText = comment.Text
            };

            var result = await _mediator.Send(command);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var newComment = _mapper.Map<ProjectCommentResponse>(result.Payload);
            return Ok(newComment);
        }
    }
}