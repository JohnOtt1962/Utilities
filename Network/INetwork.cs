namespace Utilities.Network
{
    public interface INetwork
    {
        Task<NetworkResult> SendAsync(string apiKey, string url, string request);
    }
}