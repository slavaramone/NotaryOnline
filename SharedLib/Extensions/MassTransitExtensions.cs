using MassTransit;
using MassTransit.AutofacIntegration;
using Microsoft.Extensions.Options;
using SharedLib.Options;

namespace SharedLib.Extensions
{
	public static class MassTransitExtensions
	{
		public static void RegisterMassTransitCore(this IContainerBuilderBusConfigurator busConfigurator, string queueName, bool isUseScheduler = false)
		{
			busConfigurator.AddConsumers(System.Reflection.Assembly.GetEntryAssembly());

			if (isUseScheduler)
			{
				busConfigurator.AddPublishMessageScheduler();
			}

			busConfigurator.UsingRabbitMq((context, cfg) =>
			{
				var options = context.GetRequiredService<IOptions<RabbitMQOptions>>();
				cfg.Host(options.Value.Uri);

				if (isUseScheduler)
				{
					cfg.UseInMemoryScheduler();
				}

				cfg.ReceiveEndpoint(queueName, e =>
				{
					e.Durable = true;
					e.ConfigureConsumers(context);
				});

				cfg.UseCustomValidation();
				cfg.UseExceptionLogger();
			});
		}
	}
}
