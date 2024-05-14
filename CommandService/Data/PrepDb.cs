using CommandService.Entities;
using CommandService.SyncDataService.Grpc;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                var grpcClient=serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms=grpcClient.ReturnAllPlatforms();
                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(),serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd, platforms);
            }
        }

        private static void SeedData(ICommandRepo repo,AppDbContext context, bool isProd,IEnumerable<Platform> platforms)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
            Console.WriteLine("-->seeding new platforms");
            foreach(var plat in platforms){
                 if(!repo.ExternalPlatformExists(plat.ExternalID))
                {
                    repo.CreatePlatform(plat);
                }
                repo.SaveChanges();
            }
        }
    }
}