namespace ArrestsCrimesUk.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
        }

        public static class Arrests
        {
            public const string GetAll = Base + "/arrests";
            public const string Update = Base + "/arrests{arrestId}";
            public const string Delete = Base + "/arrests{arrestId}";
            public const string GetByYear = Base + "/arrests{year}";
            public const string GetBySexAndYear = Base + "/arrests/{year}&{sex}";
            public const string GetBySexAndYearAndTown = Base + "/arrests/year&sex&town";
            public const string Create = Base + "/arrests";
        }
        
        public static class Victims
        {
            public const string GetAll = Base + "/victims";
            public const string Update = Base + "/victims{victims}";
            public const string Delete = Base + "/victims{victims}";
            public const string GetByYear = Base + "/victims{year}";
            public const string GetBySexAndYear = Base + "/victims/{year}&{sex}";
            public const string GetBySexAndYearAndTown = Base + "/victims/year&sex&town";
            public const string Create = Base + "/victims";
        }
    }
}
