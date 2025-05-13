using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

namespace WebProjectASP.Application.AIServicesRealization.Text;

public class TextGenerationMultimodal(IServiceProvider provider) : ITextGenerationMultimodal
{
    public async Task<string?> GetResponseAsync(string request, string model)
    {
        var modelInstance = provider.GetKeyedService<ITextGeneration>(model) 
                            ?? throw new InvalidOperationException($"Unable to resolve '{model}'");
        return await modelInstance.GetResponseAsync(request);
    }

    public async Task<string?> GetResponseAsync(IChat chat, string request, string model)
    {
        var modelInstance = provider.GetKeyedService<ITextGeneration>(model) 
                            ?? throw new InvalidOperationException($"Unable to resolve '{model}'");
        return await modelInstance.GetResponseAsync(chat, request);
    }
}