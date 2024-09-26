using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MinimalistDotNet;

public static partial class RegisterEndpointExtensions 
{
    /// <summary>
    /// Get every class that implements IEndpoint and isn't an abstract class.
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    internal static ServiceDescriptor[] FindEndpoints(Assembly assembly) 
    {
        return assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpointGroup)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpointGroup), type))
            .ToArray();
    }

    public static IServiceCollection AddMinimalistEndpoints(this IServiceCollection services, Assembly assembly) 
    {
        ServiceDescriptor[] serviceDescriptors = FindEndpoints(assembly);

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }


    public static IServiceCollection AddMinimalistEndpoints(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        List<ServiceDescriptor> serviceDescriptors = new();
        foreach (var assembly in assemblies)
        {
            serviceDescriptors.Concat(FindEndpoints(assembly));
        }
        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }


    public static IApplicationBuilder UseMinimalistEndpoints(this WebApplication app) 
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpointGroup>>();

        foreach (IEndpointGroup endpoint in endpoints)
        {
            endpoint.Map(app);
        }

        return app;
    }
}