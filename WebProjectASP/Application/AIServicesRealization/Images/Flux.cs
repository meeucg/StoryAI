using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images;

namespace WebProjectASP.Application.AIServicesRealization.Images;

public class Flux(string apiKey) : ImageGeneration(apiKey, "flux")
{
    private readonly HttpClient _httpClient = new()
    {
        DefaultRequestHeaders =
        {
            { "Authorization", "Bearer " + apiKey }
        }
    };
    
    public override async Task<Stream?> GetImageByDescriptionAsync(
        string description,
        CancellationToken ctx = default)
    {
        var requestBody = new
        {
            model = "black-forest-labs/FLUX.1-schnell-Free",
            prompt = description,
            width = 1024,
            height = 1024,
            steps = 4,
            n = 1,
            response_format = "b64_json",
            stops = "[]"
        };
        
        var response = await _httpClient.PostAsync(
            "https://api.together.xyz/v1/images/generations",
            new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            ),
            ctx
        );
        
        var responseContent = await response.Content.ReadAsStringAsync(ctx);
        
        try
        {
            using var jsonDoc = JsonDocument.Parse(responseContent);
            var root = jsonDoc.RootElement;
            var base64Data = root
                .GetProperty("data")[0]
                .GetProperty("b64_json")
                .GetString();
            
            var imageBytes = Convert.FromBase64String(base64Data!);
            return new MemoryStream(imageBytes);
        }
        catch
        {
            Console.WriteLine(responseContent);
            throw new ExternalException($"{Model} Failed to generate an image.");
        }
    }
}