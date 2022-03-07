using Autofac;
using AutoMapper;
using System.Reflection;

namespace SharedLib.Extensions
{
	public static class AutoMapperExtensions
	{
		public static void RegisterAutomapper(this ContainerBuilder builder)
		{
			builder.Register(_ =>
			{
				var mapperConfiguration = new MapperConfiguration(expression => ApplyMapperConfiguration(expression, Assembly.GetEntryAssembly()));

				mapperConfiguration.AssertConfigurationIsValid();

				return mapperConfiguration.CreateMapper();
			})
			.SingleInstance();
		}

		public static void ApplyMapperConfiguration(IMapperConfigurationExpression expression, Assembly assembly)
		{
			expression.AllowNullCollections = false;
			expression.AllowNullDestinationValues = true;

			expression.AddMaps(assembly);
		}
	}
}
