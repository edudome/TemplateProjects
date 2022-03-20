using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Template.Application.Utils;
using Template.Domain.AppSettings;

namespace Template.Application.Auth
{
    public class PasswordGenerator
    {
        public PasswordGenerator()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
            IConfiguration config = builder.Build();
            var configSettings = new ConfigurationSettings();
            config.GetSection(ConfigurationSettings.Configuration).Bind(configSettings);
            IOptions<ConfigurationSettings> Configuration = Options.Create(configSettings);
            Config = Configuration.Value;
        }
        private ConfigurationSettings? Config { get; set; }

        public string EncriptarPassword(string password)
        {
            // Se agrega el "salt" para que se haga más dificil la contraseña,
            // sino puede que esté en algún banco de hash/contraseñas en internet y al buscar en google el hash aparece
            string salt = Config?.SaltPasswords ?? string.Empty;
            if (string.IsNullOrEmpty(salt)) throw new Exception("No se encontró info de la SaltPassword en el appsettings.json");
            string transformedPassword = salt + password;
            string finalPassword = CryptGenerator.ComputeSha256Hash(transformedPassword);
            return "0x" + finalPassword;
        }
    }
}
