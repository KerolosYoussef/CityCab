using CityCab.Rider.API.Features.RiderManagements.Shared;

namespace CityCab.Rider.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRiderAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IRiderUniquenessChecker, RiderUniquenessChecker>();
            return services;
        }
    }
}
