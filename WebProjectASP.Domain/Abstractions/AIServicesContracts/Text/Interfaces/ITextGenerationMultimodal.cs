namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

/// <summary>
/// Provides methods for basic image generation with a specific model (from TModelsEnum)
/// </summary>
/// <typeparam name="TModelsEnum">An enum with all available text generation models.</typeparam>
public interface ITextGenerationMultimodal
{
    /// <summary>
    /// Get a response without any chat context
    /// </summary>
    /// <param name="request">Request message</param>
    /// <param name="model">LLM model from the available ones</param>
    /// <returns>A response of a LLM</returns>
    Task<string?> GetResponseAsync(string request, string model);

    /// <summary>
    /// Get a response with chat context
    /// </summary>
    /// <param name="chat">Associated chat</param>
    /// <param name="request">Request message</param>
    /// /// <param name="model">LLM model from the available ones</param>
    /// <returns>A response of a LLM, with regard to previous chat context</returns>
    Task<string?> GetResponseAsync(IChat chat, string request, string model);
}