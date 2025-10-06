using Indium.Components.ActionItems.Modules;
using Indium.Components.DraftManager.Modules;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace SDSFinder.Extensions;

public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds MVC controller used to access application health services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddHealthControllers(this IServiceCollection services)
    {
        services.AddMvcCore()

            .AddJsonOptions(config =>
            {

            });

        return services;
    }



    /// <summary>
    /// Enable the Action Items Service. Creates access to Action Item Functions. (calls to the Action Item API) 
    /// Provide a string for Action Items API route
    /// </summary>
    /// <param name="services"> Services object that action items will be added to</param>
    /// <param name="apiPath">The default endpoint path</param>
    /// <returns></returns>
    public static IServiceCollection UseActionItems(this IServiceCollection services, string apiPath = "")
    {
        services.AddHttpClient("ActionItems", httpClient =>
        {
            httpClient.BaseAddress = new Uri(apiPath);
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler()
            {
                UseDefaultCredentials = true,
            };
        });
        services.AddScoped<ApiActionItems>();

        return services;
    }
    /// <summary>
    /// Enable the Draft Manager Service. Creates access to Draft Manager Functions. (calls to the Draft Manager API) 
    /// Provide a string for Draft Manager API route
    /// </summary>
    /// <typeparam name="T">Type of the supported draft object. This will be the type provided and returned.</typeparam>
    /// <param name="services"> Services object that Draft Manager will be added to</param>
    /// <param name="apiPath">The default endpoint path for the Draft Manger API</param>
    /// <returns></returns>
    public static IServiceCollection UseDraftManager(this IServiceCollection services, string apiPath = "")
    {
        services.AddHttpClient("DraftManager", httpClient =>
        {
            httpClient.BaseAddress = new Uri(apiPath);
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler()
            {
                UseDefaultCredentials = true,
            };
        });
        services.AddScoped<ApiDraftManager>();

        return services;
    }
}
