namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;

public interface IImageGeneration
{
    /// <summary>
    /// Gets a model in a format of string
    /// </summary>
    string Model { get; }

    /// <summary>
    /// Get an image without any style specification, abstract model
    /// </summary>
    /// <param name="description">Request message</param>
    /// <param name="ctx">Cancellation token</param>
    /// <returns>Image stream</returns>
    Task<Stream?> GetImageByDescriptionAsync(string description, CancellationToken ctx = default);
}