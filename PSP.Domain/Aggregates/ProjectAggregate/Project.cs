using PSP.Domain.Aggregates.UserAggregate;
using PSP.Domain.Exceptions;
using PSP.Domain.Validators.ProjectValidators;

namespace PSP.Domain.Aggregates.ProjectAggregate {

    public class Project {
        private readonly List<ProjectComment> _comments = new List<ProjectComment>();
        private readonly List<ProjectInteraction> _interactions = new List<ProjectInteraction>();

        private Project() {
        }

        public Guid ProjectId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public string ProjectContent { get; private set; }
        public IEnumerable<ProjectComment> Comments { get { return _comments; } }
        public IEnumerable<ProjectInteraction> Interactions { get { return _interactions; } }

        // Factories

        public static Project CreateProject(Guid userId, string projectContent) {
            var validator = new ProjectValidator();

            var objectToValidate = new Project {
                UserProfileId = userId,
                ProjectContent = projectContent,
            };

            var validationResult = validator.Validate(objectToValidate);
            if (validationResult.IsValid) return objectToValidate;

            var exception = new ProjectNotValidException("Post is not valid.");
            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }

        // public methods
        /// <summary>
        /// Updates the post text
        /// </summary>
        /// <param name="newText">The updated post text</param>
        public void UpdateProjectContent(string newText) {
            if (string.IsNullOrWhiteSpace(newText)) {
                var exception = new ProjectNotValidException("Cannot update post. Post text is not valid.");
                exception.ValidationErrors.Add("The provided text is either null or contains only whitespace");
                throw exception;
            }

            ProjectContent = newText;
        }

        public void AddProjectComment(ProjectComment newComment) {
            _comments.Add(newComment);
        }

        public void RemoveComment(ProjectComment toRemove) {
            _comments.Remove(toRemove);
        }

        public void AddInteraction(ProjectInteraction newInteraction) {
            _interactions.Add(newInteraction);
        }

        public void RemoveInteraction(ProjectInteraction toRemove) {
            _interactions.Remove(toRemove);
        }
    }
}