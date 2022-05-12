namespace PSP.Api {

    public static class ApiRoutes {
        public const string BaseRoute = "api/[controller]";

        public static class UserProfiles {
            public const string IdRoute = "{id}";
        }

        public static class Projects {
            public const string IdRoute = "{id}";
            public const string ProjectComments = "{projectId}/comments";
            public const string CommentById = "{projectId}/comments/{commentId}";
        }

        public static class Identity {
            public const string Login = "login";
            public const string Registration = "registration";
        }
    }
}