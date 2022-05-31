namespace PSP.Api
{

    public static class ApiRoutes
    {
        public const string BaseRoute = "api/[controller]";

        public static class UserProfiles
        {
            public const string IdRoute = "{id}";
        }

        public static class Projects
        {
            public const string IdRoute = "{id}";
            public const string ProjectComments = "{projectId}/comments";
            public const string CommentById = "{projectId}/comments/{commentId}";
            public const string AddInteraction = "{projectId}/interactions";
            public const string InteractionById = "{projectId}/interactions/{interactionId}";
            public const string ProjectInteractions = "{projectId}/interactions";
            public const string ProjectsByCategoryId = "byCategory/{categoryId}";
        }

        public static class Identity
        {
            public const string Login = "login";
            public const string Registration = "registration";
            public const string IdentityById = "{identityUserId}";
        }

        public static class Categories {
            public const string IdRoute = "{id}";
        }
    }
}