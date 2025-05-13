using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Enums;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;

namespace WebProjectASP.Application.AIServicesRealization.Images;

public class ImageGeneratorMultimodal(IServiceProvider provider) : IImageGenerationMultimodal
{
    private readonly Dictionary<Styles, string> _styleDescriptors = new()
    {
        { Styles.Realistic, "Photorealistic style. " +
                            "Shot on a high-resolution DSLR with a 50mm prime lens " +
                            "(f/8 aperture, ISO 100, 1/125s exposure)." +
                            "NO TEXT!" },
        { Styles.Manga, "Kentaro Miura manga style, black and white, one panel, no text." },
        { Styles.Comics, "Marvel comics style, no text, one panel. Drawn by hand, bold strokes, outlined objects, vivid colors, high contrast" },
        { Styles.NoStyle, "" }
    };

    public async Task<Stream?> GetImageByDescriptionAsync(
        string description, 
        string model, 
        CancellationToken ctx = default)
    {
        for (var i = 0; i < 5; i++)
        {
            try
            {
                var modelInstance = provider.GetRequiredKeyedService<IImageGeneration>(model);
                var result = await modelInstance.GetImageByDescriptionAsync(description, ctx);
                return result;
            }
            catch(Exception ex)
            {
                Thread.Sleep(5000);
            }
        }
        return null;
    }

    public async Task<Stream?> GetImageByDescriptionAsync(
        string description, 
        Styles style, 
        string model, 
        CancellationToken ctx = default)
    {
        return await GetImageByDescriptionAsync(
            description[^1] == '.' ? description + " " + _styleDescriptors[style]
                : description + ". " + _styleDescriptors[style], 
            model, 
            ctx);
    }
}