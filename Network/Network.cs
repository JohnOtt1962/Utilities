using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Utilities.Network
{
    public class Network : INetwork
    {
        private static readonly HttpClient Client = new HttpClient()
        {
            Timeout = TimeSpan.FromMinutes(6)
        };
        
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<NetworkResult> SendAsync(string apiKey, string url, string request)
        {
            NetworkResult? result = null;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            HttpResponseMessage response = await Client.SendAsync(GetRequestMessage(url, request));
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(responseString))
            {
                result = HydrateResult(responseString, true);
            }
            else
            {
                result = HydrateResult(responseString, false);
            }

            return result;
        }

        private HttpRequestMessage GetRequestMessage(string url, string jsonPayload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            return request;
        }

        private NetworkResult HydrateResult(string responseString, bool isError)
        {
            return new NetworkResult
            {
                JsonResponse = responseString,
                CallSuccess = isError
            };
        }
    }
}