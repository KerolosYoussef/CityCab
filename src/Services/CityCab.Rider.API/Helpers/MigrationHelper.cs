namespace CityCab.Rider.API.Helpers
{
    public static class MigrationHelper
    {
        public static async Task MigrateData(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                logger.LogInformation("Migrating database...");
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
                logger.LogInformation("Database migrated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating database");
            }
        }
    }
}
