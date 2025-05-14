using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Enums;

namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;

public interface IImageGenerationMultimodal
{
    /// <summary>
    /// Get an image without any style specification
    /// </summary>
    /// <param name="description">Request message</param>
    /// <param name="model">Image generation model in use</param>
    /// <param name="ctx">Cancellation token</param>
    /// <returns>Image stream</returns>
    Task<Stream?> GetImageByDescriptionAsync(string description,
        string model,
        CancellationToken ctx = default);

    /// <summary>
    /// Get an image in a specific style (will add a style description text to a request message).
    /// Doesn't garantee that a resulting image will match a style 100%, depends on a model in use
    /// </summary>
    /// <param name="description">Request message</param>
    /// <param name="style">A style from the available ones</param>
    /// <param name="model">Image generation model</param>
    /// /// <param name="ctx">Cancellation token</param>
    /// <returns>Image stream</returns>
    Task<Stream?> GetImageByDescriptionAsync(
        string description, 
        Styles style,
        string model,
        CancellationToken ctx = default);
}
