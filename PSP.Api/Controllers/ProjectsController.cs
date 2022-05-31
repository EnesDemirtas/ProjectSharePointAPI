using Microsoft.AspNetCore.Authorization;
using PSP.Api.Extensions;
using ProjectInteraction = PSP.Api.Contracts.Projects.Responses.ProjectInteraction;

namespace PSP.Api.Controllers
{

    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _mediator.Send(new GetAllProjects());
            var mapped = _mapper.Map<List<ProjectResponse>>(result.Payload);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetProjectById(string id)
        {
            var projectId = Guid.Parse(id);
            var query = new GetProjectById { ProjectId = projectId };
            var result = await _mediator.Send(query);

            var mapped = _mapper.Map<ProjectResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.ProjectsByCategoryId)]
        [ValidateGuid("categoryId")]
        public async Task<IActionResult> GetProjectsByCategoryId(string categoryId)
        {
            var categoryGuid = Guid.Parse(categoryId);

            var query = new GetProjectsByCategoryId { CategoryId = categoryGuid };
            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<List<ProjectResponse>>(result.Payload);
            return result.IsError? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreate newPost)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new CreateProject()
            {
                UserProfileId = userProfileId,
                ProjectName = newPost.ProjectName,
                CategoryId = newPost.CategoryId,
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> UpdateProjectText([FromBody] ProjectUpdate updatedPost, string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new UpdateProjectContent()
            {
                NewText = updatedPost.Text,
                ProjectId = Guid.Parse(id),
                UserProfileId = userProfileId
            };

            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.IdRoute)]
        [ValidateGuid("id")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> DeleteProject(string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new DeleteProject() { ProjectId = Guid.Parse(id), UserProfileId = userProfileId };
            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.ProjectComments)]
        [ValidateGuid("projectId")]
        public async Task<IActionResult> GetCommentsByProjectId(string projectId)
        {
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> AddCommentToProject(string projectId, [FromBody] ProjectCommentCreate comment)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new AddProjectComment()
            {
                ProjectId = Guid.Parse(projectId),
                UserProfileId = userProfileId,
                CommentText = comment.Text
            };

            var result = await _mediator.Send(command);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var newComment = _mapper.Map<ProjectCommentResponse>(result.Payload);
            return Ok(newComment);
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.CommentById)]
        [ValidateGuid("postId", "commentId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> RemoveCommentFromPost(string postId, string commentId, CancellationToken token)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var postGuid = Guid.Parse(postId);
            var commentGuid = Guid.Parse(commentId);
            var command = new RemoveCommentFromProject
            {
                UserProfileId = userProfileId,
                CommentId = commentGuid,
                ProjectId = postGuid
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpPut]
        [Route(ApiRoutes.Projects.CommentById)]
        [ValidateGuid("postId", "commentId")]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> UpdateCommentText(string postId, string commentId, ProjectCommentUpdate updatedComment
            , CancellationToken token)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var postGuid = Guid.Parse(postId);
            var commentGuid = Guid.Parse(commentId);
            var command = new UpdateProjectComment
            {
                UserProfileId = userProfileId,
                ProjectId = postGuid,
                CommentId = commentGuid,
                UpdatedText = updatedComment.Text
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpGet]
        [Route(ApiRoutes.Projects.ProjectInteractions)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetPostInteractions(string postId, CancellationToken token)
        {
            var postGuid = Guid.Parse(postId);
            var query = new GetProjectInteractions { ProjectId = postGuid };
            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<List<ProjectInteraction>>(result.Payload);
            return Ok(mapped);
        }

        [HttpPost]
        [Route(ApiRoutes.Projects.ProjectInteractions)]
        [ValidateGuid("postId")]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> AddPostInteraction(string postId, ProjectInteractionCreate interaction,
            CancellationToken token)
        {
            var postGuid = Guid.Parse(postId);
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new AddInteraction
            {
                ProjectId = postGuid,
                UserProfileId = userProfileId,
                Type = interaction.Type
            };

            var result = await _mediator.Send(command, token);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<ProjectInteraction>(result.Payload);
            return Ok(mapped);
        }

        [HttpDelete]
        [Route(ApiRoutes.Projects.InteractionById)]
        [ValidateGuid("postId", "interactionId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> RemovePostInteraction(string postId, string interactionId, CancellationToken token)
        {
            var postGuid = Guid.Parse(postId);
            var interactionGuid = Guid.Parse(interactionId);
            var userProfileGuid = HttpContext.GetUserProfileIdClaimValue();

            var command = new RemoveProjectInteraction
            {
                ProjectId = postGuid,
                InteractionId = interactionGuid,
                UserProfileId = userProfileGuid
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<ProjectInteraction>(result.Payload);
            return Ok(mapped);
        }
    }
}