
using Microsoft.AspNetCore;
using Notes;
using Notes.Repository.InterfacesOfStorage;
using Notes.TestDbSQLite;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = BuildWebHost(args);

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json"); 
        builder.Build(); 
        host.Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
       WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();
}