using System.ClientModel;
using OpenAI;
using OpenAI.Chat;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

namespace WebProjectASP.Application.AIServicesRealization.Text;

public class QwenTextGeneration(string apiKey) : TextGeneration("qwen")
{
    private readonly ChatClient _client = new(
        model: "Qwen/Qwen3-235B-A22B-fp8-tput", 
        credential: new ApiKeyCredential(apiKey),
        options: new OpenAIClientOptions
        {
            Endpoint = new Uri("https://api.together.xyz/v1"),
        }
    );
    
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
        
        var response = await _client.CompleteChatAsync(convertedHistory, 
            OpenAIStaticValues.Options);   
        return response.Value.Content[0].Text;
    }
}