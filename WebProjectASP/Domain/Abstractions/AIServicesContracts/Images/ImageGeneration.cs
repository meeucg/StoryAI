using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;

namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Images;

public abstract class ImageGeneration(string apiKey, string model) : IImageGeneration
{
    public string Model => model;
    public abstract Task<Stream?> GetImageByDescriptionAsync(string description, CancellationToken ctx = default);
}