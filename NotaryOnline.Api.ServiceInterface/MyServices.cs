using ServiceStack;
using NotaryOnline.Api.ServiceModel;

namespace NotaryOnline.Api.ServiceInterface;

public class MyServices : Service
{
    public object Any(Hello request)
    {
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}