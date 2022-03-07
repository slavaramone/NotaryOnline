using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLib.Extensions;
using System;

namespace SharedLib.Templates
{
	public class StartupTemplate<TStartup>  where TStartup : class
	{
		public IConfiguration Configuration { get; protected set; }

		public IServiceProvider ConfigureTemplateServices(IServiceCollection services, string swaggerTitle, string xmlFileName)
		{
			services.AddCors()
				.AddMvc(options =>
				{
					options.EnableEndpointRouting = false;
				})
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
					options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
				}); ;

			services.ConfigureMassTransitAspNetOptions(Configuration);

			services.ConfigureSwagger(swaggerTitle, xmlFileName);

			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterAssemblyModules(typeof(TStartup).Assembly);
			builder.Populate(services);

			var container = builder.Build();
			return new AutofacServiceProvider(container);
		}

		public void ConfigureTemplate(IApplicationBuilder app, IWebHostEnvironment env, string swaggerTitle)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseAuthentication();

			app.UseRequestResponseLogging();

			app.UseSwaggerConfiguration(swaggerTitle);

			app.ConfigureExceptionHandlingMiddleware();

			app.UseRouting();
			app.UseMvc();
		}
	}
}
