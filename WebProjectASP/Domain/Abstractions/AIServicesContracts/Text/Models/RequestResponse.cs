namespace WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Models;

/// <summary>
/// Standardized class descriptor of a request/response pair to an LLM
/// </summary>
public struct RequestResponse
{
    public DateTime RequestTimestamp { get; set; }
    public DateTime ResponseTimestamp { get; set; }
    public required string Request { get; set; }
    public required string Response { get; set; }
}
