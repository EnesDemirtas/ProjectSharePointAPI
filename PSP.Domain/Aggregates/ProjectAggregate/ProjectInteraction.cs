using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Domain.Aggregates.ProjectAggregate
{

    public class ProjectInteraction
    {

        private ProjectInteraction()
        {
        }

        public Guid InteractionId { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid? UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }

        public InteractionType InteractionType { get; private set; }

        // Factories

        public static ProjectInteraction CreateProjectInteraction(Guid projectId, Guid userProfileId, InteractionType type)
        {
            return new ProjectInteraction
            {
                ProjectId = projectId,
                UserProfileId = userProfileId,
                InteractionType = type
            };
        }
    }
}