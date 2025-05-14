using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebProjectASP.Application.AIServicesRealization.Images;
using WebProjectASP.Application.AIServicesRealization.Text;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

namespace WebProjectASP.Application.ServiceBuilders.AIServicesBuilder;

public static class OpenAIServices
{
    public static void AddOpenAITextServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["ApiKeys:OpenAI"] ?? throw new Exception("No API key for OpenAI services");
        
        services.AddKeyedSingleton<ITextGeneration>("gpt-4o", (provider, obj) =>
            new OpenAiTextGeneration(
                apiKey,
                "gpt-4o"
            ));
        
        services.AddKeyedSingleton<ITextGeneration>("o3-mini", (provider, obj) =>
            new OpenAiTextGeneration(
                apiKey,
                "o3-mini"
            ));
        
        services.AddKeyedSingleton<ITextGeneration>("o4-mini", (provider, obj) =>
            new OpenAiTextGeneration(
                apiKey,
                "o4-mini"
            ));
        
        services.AddKeyedSingleton<ITextGeneration>("gpt-3.5-turbo", (provider, obj) =>
            new OpenAiTextGeneration(
                apiKey,
                "gpt-3.5-turbo"
            ));
    }
    
    public static void AddOpenAIImageServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["ApiKeys:OpenAI"] ?? throw new Exception("No API key for OpenAI services");
        
        services.AddKeyedSingleton<IImageGeneration>("dall-e-3", (provider, obj) => 
            new Dalle3(apiKey));

        services.AddKeyedSingleton<IImageGeneration>("dall-e-2", (provider, obj) =>
            new Dalle2(apiKey));
    }
}