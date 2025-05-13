using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Models;

namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;

/// <summary>
/// A facade for using LLM as a chat with ability to remember context
/// </summary>
public interface IChat
{
    List<RequestResponse> GetChatHistory();
    RequestResponse GetLast();
    Task<string> NewMessage(string request, string model);
}
