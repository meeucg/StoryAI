using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebProjectASP.Application.AIServicesRealization.Images;
using WebProjectASP.Application.AIServicesRealization.Text;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

namespace WebProjectASP.Application.ServiceBuilders.AIServicesBuilder;

public static class TogetherAIServices
{
    public static void AddTogetherAIImageServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["ApiKeys:TogetherAI"] ?? throw new Exception("No API key for TogetherAI services");
        
        services.AddKeyedSingleton<IImageGeneration>("flux", (provider, obj) =>
            new Flux(apiKey));
    }
    
    public static void AddTogetherAITextServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["ApiKeys:TogetherAI"] ?? throw new Exception("No API key for TogetherAI services");
        
        services.AddKeyedSingleton<ITextGeneration>("qwen", (provider, obj) =>
            new QwenTextGeneration(apiKey));
    }
}