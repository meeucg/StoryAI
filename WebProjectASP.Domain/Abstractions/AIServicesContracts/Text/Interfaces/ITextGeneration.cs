namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

/// <summary>
/// Provides methods for basic image generation with an unspecified model
/// </summary>
public interface ITextGeneration
{
    /// <summary>
    /// Gets a model name
    /// </summary>
    string Model { get; }
    /// <summary>
    /// Get a response without any chat context
    /// </summary>
    /// <param name="request">Request message</param>
    /// <returns>A response of a LLM</returns>
    Task<string?> GetResponseAsync(string request);

    /// <summary>
    /// Get a response with chat context
    /// </summary>
    /// <param name="chat">Associated chat</param>
    /// <param name="request">Request message</param>
    /// <returns>A response of a LLM, with regard to previous chat context</returns>
    Task<string?> GetResponseAsync(IChat chat, string request);
}