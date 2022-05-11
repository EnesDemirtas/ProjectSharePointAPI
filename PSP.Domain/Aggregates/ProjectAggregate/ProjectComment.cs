using PSP.Domain.Exceptions;
using PSP.Domain.Validators.ProjectValidators;

namespace PSP.Domain.Aggregates.ProjectAggregate {

    public class ProjectComment {

        private ProjectComment() {
        }

        public Guid CommentId { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Text { get; private set; }
        public Guid UserId { get; private set; }

        // Factories

        public static ProjectComment CreateProjectComment(Guid projectId, string text, Guid userId) {
            var validator = new ProjectCommentValidator();

            var objectToValidate = new ProjectComment {
                ProjectId = projectId,
                Text = text,
                UserId = userId
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exception = new ProjectCommentNotValidException("Post comment is not valid.");

            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }

        // public methods
        public void UpdateCommentText(string newText) {
            Text = newText;
        }
    }
}