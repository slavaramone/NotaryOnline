using Funq;
using NotaryOnline.Api.ServiceInterface;
using NotaryOnline.DataAccess;
using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.DataAccess.Repositories;
using ServiceStack.Text;
using SharedLib;
using SharedLib.Extensions;
using SharedLib.Mongo;
using SharedLib.Options;
using SharedLib.Security;

[assembly: HostingStartup(typeof(AppHost))]

namespace NotaryOnline.Api;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => {

            Licensing.RegisterLicense("Individual (c) 2022 vromanov00@gmail.com Tcui4gefkbQ/7S9BywCV4FoolarnUKo1LKhtE9SNe7JsKglg06Ikh6g6z0DsFvFi+rX3cqcNIgGQwO0LUX5TGmMSbS+zOndzSZ86n+yQlikUZDeu/YGoJ2ik+7qKbpvMqgVM9zGW3jD2eT1R6jus6ekT16YavtxRfg7uMnc7p/4=");

            var base64Key = Convert.ToBase64String(AesUtils.CreateKey());
            string connectionString = context.Configuration[Constants.DbConnectionString];
            services.InitOrmLiteDb(connectionString);
            //services.ApplyFluentMigrations<AddTypeToDevice>(connectionString);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.Configure<AuthOptions>(context.Configuration.GetSection(AuthOptions.Auth));
            services.Configure<MongoDbOptions>(context.Configuration.GetSection(MongoDbOptions.MongoDb));

            services.ConfigureAuthentication(context.Configuration);

            services.ConfigureLogging(context.Configuration);

            services.AddSingleton<IUserProfileRepository, UserProfileRepository>();
            services.AddSingleton<IJwtAuthFactory, JwtAuthFactory>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IOrderDocumentRepository, OrderDocumentRepository>();
            services.AddSingleton<IAuthSessionRepository, AuthSessionRepository>();
            services.AddSingleton<DocumentStorage>();
        });

    public AppHost() : base("NotaryOnline.Api", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        JsConfig<Guid>.SerializeFn = guid => guid.ToString();
        JsConfig<Guid?>.SerializeFn = guid => guid.ToString();
        //JsConfig<DateTime>.SerializeFn = dt => dt.ToString();
        //JsConfig<DateTime?>.SerializeFn = dt => dt.ToString();

        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
            DefaultContentType = MimeTypes.Json,
            AddRedirectParamsToQueryString = true
        });
    }
}
