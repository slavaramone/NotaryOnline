using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using SharedLib.Extensions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SharedLib.Templates
{
	public abstract class ProgramTemplate
	{
		public static Task Init(string[] args, Assembly assembly, Action<HostBuilderContext, IServiceCollection> configureDelegate)
		{
			return new HostBuilder()
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureHostConfiguration(config =>
				{
					config.AddJsonFile("appsettings.json", optional: true);
					config.AddEnvironmentVariables();
				})
				.ConfigureLogging(opts =>
				{
					opts.AddNLog();
				})
				.ConfigureServices(configureDelegate)
				.ConfigureContainer<ContainerBuilder>((context, builder) =>
				{
					builder.RegisterAssemblyModules(assembly);
				})
				.UseConsoleLifetime()
				.RunConsoleAsync();
		}
	}
}
