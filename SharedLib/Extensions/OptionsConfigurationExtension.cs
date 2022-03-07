using MassTransit;
using MassTransit.Scoping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLib.Options;

namespace SharedLib.Extensions
{
	public static class OptionsConfigurationExtension
	{
		public static IServiceCollection ConfigureMassTransitConsoleOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions()
				.Configure<RabbitMQOptions>(options => configuration.GetSection(RabbitMQOptions.RabbitMQ).Bind(options))
				.AddHostedService<MassTransitHostedService>();
			return services;
		}

		public static IServiceCollection ConfigureMassTransitAspNetOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions()
				.Configure<RabbitMQOptions>(options => configuration.GetSection(RabbitMQOptions.RabbitMQ).Bind(options));
			
			services.AddMassTransitHostedService();
			services.AddScoped<ScopedConsumeContextProvider>();
			return services;
		}
	}
}
