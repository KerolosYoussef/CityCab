using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CityCab.Messaging.Common.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
        {
            services.AddMassTransit(config =>
            {
                ConfigureMassTransit(configuration, assembly, config);
            });
            return services;
        }

        public static IServiceCollection AddMessageBrokerWithOutbox<T>(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
            where T : DbContext
        {
            services.AddMassTransit(config =>
            {
                config.AddEntityFrameworkOutbox<T>(o =>
                {
                    o.UsePostgres();
                    o.UseBusOutbox();
                });

                ConfigureMassTransit(configuration, assembly, config);
            });
            return services;
        }

        private static void ConfigureMassTransit(IConfiguration configuration, Assembly? assembly, IBusRegistrationConfigurator config)
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                var connectionString = configuration.GetConnectionString("messaging");

                ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

                configurator.Host(new Uri(connectionString));

                configurator.ConfigureEndpoints(context);
            });
        }
    }
}
