using Microsoft.Extensions.DependencyInjection;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;

namespace NotaryOnline.DataAccess
{
	public static class DbSetup
	{
		public static IServiceCollection InitOrmLiteDb(this IServiceCollection services, string connectionString)
		{
			var dbFactory = new OrmLiteConnectionFactory(connectionString, PostgreSqlDialect.Provider);
			using (var db = dbFactory.Open())
			{
				OrmLiteConfig.DialectProvider.NamingStrategy = new AliasNamingStrategy();

				Guid user1Id = new("aae310f2-ba89-4a71-87f2-ef407677e8ce");
				Guid user2Id = new("bbe310f2-ba89-4a71-87f2-ef407677e8ce");
				Guid user3Id = new("cce310f2-ba89-4a71-87f2-ef407677e8ce");
				if (db.CreateTableIfNotExists<User>())
				{
					db.Insert(new User { Id = user1Id, Name = "Стогов", Role = UserRole.Admin, Email = "dummy1@ya.ru", Phone = "+79871111111",
						PasswordHash = "7jnQObemzr1p63nsLrCsSw4i+Gu4dnNAKmu0KV+yv9A=", PasswordSalt = "11111111", DeviceUid = "id1", IsActive = true });

					db.Insert(new User { Id = user2Id, Name = "Петров", Role = UserRole.Translator, Email = "dummy2@ya.ru", Phone = "+79872222222",
						PasswordHash = "2aaaf4b29ec7c6280186a152e12bb115cdaa3f182d382555896f58be728a7419", PasswordSalt = "22222222", DeviceUid = "id2", IsActive = true });

					db.Insert(new User { Id = user3Id, Name = "Сидоров", Role = UserRole.Customer, Email = "dummy3@ya.ru", Phone = "+79873333333",
						PasswordHash = "2aaaf4b29ec7c6280186a152e12bb115cdaa3f182d382555896f58be728a7419", PasswordSalt = "33333333", DeviceUid = "id3", IsActive = true });
				}

				if (db.CreateTableIfNotExists<UserProfile>())
				{
					db.Insert(new UserProfile { Surname = "Сидоров", Name = "Имя", MiddleName = "Отчество", DateOfBirth = new DateTime(1999, 1, 1), City = "Спб", Address = "Смольный, 1",
						DocNumber = "213431SX931", DocIssueDepartment = "УВД", DocIssueCode = "XXX-TTT", DocIssueCountry = "Литва", DocIssueDate = new DateTime(2019, 1, 1), 
						DocValidTo = new DateTime(2019, 1, 1), UserId = user3Id });
				}
				db.CreateTableIfNotExists<AuthSession>();
				db.CreateTableIfNotExists<Order>();
				db.CreateTableIfNotExists<OrderDocument>();
			}

			services.AddSingleton<IDbConnectionFactory>(dbFactory);
			return services;
		}
	}
}
