namespace Template.Domain.AppSettings
{
    public class ConfigurationSettings
    {
        public const string Configuration = "Configuration";

        public ConnectionStrings? ConnectionStrings { get; set; }

        public JWT? JWT { get; set; }

        public string SaltPasswords { get; set; } = default!;

        public bool EsUnitTest { get; set; } = default!;
    }

    public class ConnectionStrings
    {
        public string SqlDB { get; set; } = default!;

        public string MongoDB { get; set; } = default!;
    }

    public class JWT
    {
        public string SecretKey { get; set; } = default!;

        public int ExpiresInHours { get; set; } = 8;

        public string ValidAudience { get; set; } = default!;

        public string ValidIssuer { get; set; } = default!;

    }
}
