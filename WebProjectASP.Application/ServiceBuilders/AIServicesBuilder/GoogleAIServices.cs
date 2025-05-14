using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebProjectASP.Application.AIServicesRealization.Images;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;

namespace WebProjectASP.Application.ServiceBuilders.AIServicesBuilder;

public static class GoogleAIServices
{
    public static void AddGoogleImageServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["ApiKeys:Google"] ?? throw new Exception("No API key for Google services");
        
        services.AddKeyedSingleton<IImageGeneration>("gemini", (provider, obj) => 
            new GeminiImages(apiKey));
    }
}