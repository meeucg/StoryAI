using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images;

namespace WebProjectASP.Application.AIServicesRealization.Images;

public class GeminiImages(string apiKey) : ImageGeneration(apiKey, "gemini")
{
    private readonly HttpClient _httpClient = new();
    
    public override async Task<Stream?> GetImageByDescriptionAsync(
        string description,
        CancellationToken ctx = default)
    {
        var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/" +
                         $"gemini-2.0-flash-exp-image-generation:generateContent?key={apiKey}";
        
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = description + " IMPORTANT: 4K Resolution, SQUARED image, 4096x4096, Aspect ratio - 1:1" }
                    }
                }
            },
            generationConfig = new
            {
                responseModalities = new[] { "TEXT", "IMAGE" }
            }
        };
        
        try
        {
            var response = await _httpClient.PostAsync(
                requestUrl,
                new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                ),
                ctx
            );

            var responseContent = await response.Content.ReadAsStringAsync(ctx);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(responseContent);
                throw new Exception();
            }

            Console.WriteLine(JsonStructureGenerator.GenerateStructure(responseContent));
            
            using var jsonDoc = JsonDocument.Parse(responseContent);
            var root = jsonDoc.RootElement;
            var part = root
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0];

            var base64Data = "";
            
            try
            {
                base64Data = part.GetProperty("inlineData")
                    .GetProperty("data")
                    .GetString();
            }
            catch
            {
                base64Data = root
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[1]
                    .GetProperty("inlineData")
                    .GetProperty("data")
                    .GetString();;
            }

            // Console.WriteLine(base64Data);
            
            var imageBytes = Convert.FromBase64String(base64Data!);
            return new MemoryStream(imageBytes);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message, ex.StackTrace);
            throw new ExternalException($"{Model} Failed to generate an image.");
        }
    }
}

public class JsonStructureGenerator
{
    public static string GenerateStructure(string json)
    {
        using JsonDocument document = JsonDocument.Parse(json);
        object structure = ProcessElement(document.RootElement);
        return JsonSerializer.Serialize(structure, new JsonSerializerOptions { WriteIndented = true });
    }

    private static object ProcessElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                var obj = new Dictionary<string, object>();
                foreach (var property in element.EnumerateObject())
                {
                    obj[property.Name] = ProcessElement(property.Value);
                }
                return obj;
            case JsonValueKind.Array:
                var array = new List<object>();
                foreach (var item in element.EnumerateArray())
                {
                    array.Add(ProcessElement(item));
                }
                return array;
            default:
                // Replace all values with "placeholder"
                return "placeholder";
        }
    }
}