using System.ClientModel;
using System.Runtime.InteropServices;
using OpenAI;
using OpenAI.Images;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images;

namespace WebProjectASP.Application.AIServicesRealization.Images;

public class Dalle2(string apiKey) : ImageGeneration(apiKey, "dall-e-2")
{
    private readonly ImageClient _dalleClient = new(
        model:"dall-e-2",
        credential: new ApiKeyCredential(apiKey),
        options: new OpenAIClientOptions
        {
            // Endpoint = new Uri("https://hubai.loe.gg/v1"),
            Endpoint = new Uri("https://api.aiguoguo199.com/v1"),
        });

    public override async Task<Stream?> GetImageByDescriptionAsync(
        string description, 
        CancellationToken ctx = default)
    {
        ImageGenerationOptions options = new()
        {
            Quality = GeneratedImageQuality.Standard,
            Size = GeneratedImageSize.W1024xH1024,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Bytes
        };

        try
        {
            GeneratedImage image = await _dalleClient.GenerateImageAsync(description, options, ctx);
            BinaryData bytes = image.ImageBytes;

            return bytes.ToStream();
        }
        catch
        {
            throw new ExternalException($"{Model} Failed to generate an image.");
        }
    }
}