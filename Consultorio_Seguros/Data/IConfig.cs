namespace Consultorio_Seguros.Data
{
    public class IConfig
    {
        public static IConfiguration Config { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");

            Config = builder.Build();
            return Config.GetConnectionString("db");
        }
    }
}
