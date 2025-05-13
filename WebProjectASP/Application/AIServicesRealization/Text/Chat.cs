using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Models;

namespace WebProjectASP.Application.AIServicesRealization.Text;

public class Chat(ITextGenerationMultimodal textGen) : IChat
{
    private readonly List<RequestResponse> _chatHistory = [];
    public List<RequestResponse> GetChatHistory() => _chatHistory;

    public RequestResponse GetLast() => _chatHistory[^1];

    public async Task<string> NewMessage(string request, string model)
    {
        var requestTimestamp = DateTime.Now;
        var response = await textGen.GetResponseAsync(this, request, model) ?? "No response";

        var newRr = new RequestResponse
        {
            Request = request,
            Response = response,
            RequestTimestamp = requestTimestamp,
            ResponseTimestamp = DateTime.Now
        };
        _chatHistory.Add(newRr);
        
        return newRr.Response;
    }
}