using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;
using WebProjectASP.Application.AIServicesRealization.Images;

namespace WebProjectASP.Configuration.AIServicesBuilder;

public static class GoogleAIServices
{
    public static void AddGoogleImageServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["ApiKeys:Google"] ?? throw new Exception("No API key for Google services");
        
        services.AddKeyedSingleton<IImageGeneration>("gemini", (provider, obj) => 
            new GeminiImages(apiKey));
    }
}