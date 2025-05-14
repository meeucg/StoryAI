using System.ClientModel;
using OpenAI;
using OpenAI.Chat;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

namespace WebProjectASP.Application.AIServicesRealization.Text;

public class OpenAiTextGeneration(string apiKey, string model) : 
    TextGeneration(ValidateModel(model) ? 
        model : throw new ArgumentException($"OpenAI {model} model is not available"))
{
    private static bool ValidateModel(string model) => AvailableModels.Contains(model);
    
    private readonly ChatClient _client = new(
        model: model, 
        credential: new ApiKeyCredential(apiKey),
        options: new OpenAIClientOptions
        {
            // Endpoint = new Uri("https://hubai.loe.gg/v1"),
            Endpoint = new Uri("https://api.aiguoguo199.com/v1"),
        }
    );
    
    private static readonly List<string> AvailableModels =
    [
        "gpt-4o",
        "o3-mini",
        "o4-mini",
        "gpt-3.5-turbo"
    ];

    public override async Task<string?> GetResponseAsync(string request)
    {
        var response = await _client.CompleteChatAsync(request);
        return response.Value.Content[0].Text;
    }

    public override async Task<string?> GetResponseAsync(IChat chat, string request)
    {
        var chatHistory = chat.GetChatHistory();
        
        var convertedHistory = new List<ChatMessage>();
        foreach (var chatRequestResponse in chatHistory)
        {
            convertedHistory.Add(new UserChatMessage(chatRequestResponse.Request));
            convertedHistory.Add(new AssistantChatMessage(chatRequestResponse.Response));
        }
        convertedHistory.Add(new UserChatMessage(request));

        if (model == "gpt-4o")
        {
            var response = await _client.CompleteChatAsync(convertedHistory, 
                OpenAIStaticValues.Options);   
            return response.Value.Content[0].Text;
        }
        else
        {
            var response = await _client.CompleteChatAsync(convertedHistory); 
            return response.Value.Content[0].Text;
        }
    }
}

public static class OpenAIStaticValues
{
    public static readonly ChatCompletionOptions Options = new()
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "story_output",
                jsonSchema: BinaryData.FromBytes("""
                                                 {
                                                   "$schema": "http://json-schema.org/draft-07/schema#",
                                                   "title": "story_output",
                                                   "type": "object",
                                                   "properties": {
                                                     "description": {
                                                       "type": "string",
                                                       "description": "Atmospheric narrative text describing the current scene"
                                                     },
                                                     "img": {
                                                       "type": "string",
                                                       "description": "Vivid visual description"
                                                     },
                                                     "options": {
                                                       "type": "array",
                                                       "items": {
                                                         "type": "string",
                                                         "description": "Player choice option with action prefix"
                                                       }
                                                     },
                                                     "continuityNotes": {
                                                        "type": "array",
                                                        "items": {
                                                            "type": "string",
                                                            "description": "Notes for story continuity and consistency"
                                                        }
                                                     }
                                                   },
                                                   "required": ["description", "img", "options", "continuityNotes"],
                                                   "additionalProperties": false
                                                 }
                                                 """u8.ToArray()),
                jsonSchemaIsStrict: true)
        };
}
