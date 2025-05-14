using System.ClientModel;
using OpenAI;
using OpenAI.Images;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images;

namespace WebProjectASP.Application.AIServicesRealization.Images;

public class Dalle3(string apiKey) : ImageGeneration(apiKey, "dall-e-3")
{
    private readonly ImageClient _dalleClient = new(
        model:"dall-e-3",
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
            Size = GeneratedImageSize.W1024xH1792,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Bytes
        };

        try
        {
            GeneratedImage image = await _dalleClient.GenerateImageAsync(
                "I NEED to test how the tool works with extremely simple prompts. DO NOT add any detail, just use it AS-IS: " + 
                description + " !!!NO TEXT AT ALL!!!", 
                options, 
                ctx);
        
            BinaryData bytes = image.ImageBytes;
            
            return bytes.ToStream();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine($"{Model} Failed to generate image");
        }

        return null;
    }
}