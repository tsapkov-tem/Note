using Notes.Repository.InterfacesOfStorage;
using Notes.TestDbSQLite;
using Microsoft.Extensions.Logging;
using NLog.Targets;
using NLog;

namespace Notes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            NLogCreate();
            services.AddControllers();

            services.AddScoped<IStorage, SQLiteStorage>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void NLogCreate()
        {
            var target = new FileTarget();
            var path = Directory.GetCurrentDirectory();
            target.FileName = $"{path}\\Logs\\Log.txt";
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, NLog.LogLevel.Info);
            Logger logger = LogManager.GetLogger("main");
            logger.Info("App is starting...");
        }
    }
}
