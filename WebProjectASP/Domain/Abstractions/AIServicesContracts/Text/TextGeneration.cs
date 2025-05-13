using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Text;

public abstract class TextGeneration(string model) : ITextGeneration
{
    public string Model => model;
    
    public abstract Task<string?> GetResponseAsync(string request);

    public abstract Task<string?> GetResponseAsync(IChat chat, string request);
}