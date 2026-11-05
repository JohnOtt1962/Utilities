using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Utilities.Network
{
    public class Network : INetwork
    {
        private static readonly HttpClient Client = new HttpClient()
        {
            Timeout = TimeSpan.FromMinutes(20)
        };
        
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<NetworkResult> SendAsync(string apiKey, string url, string request)
        {
            NetworkResult? result = null;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            using HttpRequestMessage requestMessage = GetRequestMessage(url, request, apiKey);

            using HttpResponseMessage response = await Client.SendAsync(requestMessage);

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

        private HttpRequestMessage GetRequestMessage(string url, string jsonPayload, string apiKey)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            return request;
        }

        private NetworkResult HydrateResult(string responseString, bool isSuccess)
        {
            return new NetworkResult
            {
                JsonResponse = responseString,
                CallSuccess = isSuccess
            };
        }
    }
}