using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ECommerce.WebUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress =
                new Uri("https://localhost:7189/api/");
        }

        public async Task<T?> GetAsync<T>(string url, string? token = null)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response =
                await _httpClient.GetAsync(url);

            string json =
     await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    string.IsNullOrWhiteSpace(json)
                        ? "API Error"
                        : json);
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)json;
            }

            return JsonSerializer.Deserialize<T>(
                json,
                JsonOptions);
        }

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        public async Task<T?> PostAsync<T>(
            string url,
            object data,
            string? token = null,
            bool ensureSuccess = false)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            StringContent content =
                new(
                    JsonSerializer.Serialize(data, JsonOptions),
                    Encoding.UTF8,
                    "application/json");

            HttpResponseMessage response =
                await _httpClient.PostAsync(url, content);

            string json =
                await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    string.IsNullOrWhiteSpace(json)
                        ? "API Error"
                        : json);
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)json;
            }

            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        public async Task<T?> PutAsync<T>(
            string url,
            object data,
            string? token = null,
            bool ensureSuccess = false)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            StringContent content =
                new(
                    JsonSerializer.Serialize(data, JsonOptions),
                    Encoding.UTF8,
                    "application/json");

            HttpResponseMessage response =
                await _httpClient.PutAsync(url, content);

            string json =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    string.IsNullOrWhiteSpace(json)
                        ? "API Error"
                        : json);
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)json;
            }

            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        public async Task DeleteAsync(
            string url,
            string? token = null,
            bool ensureSuccess = false)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response =
                await _httpClient.DeleteAsync(url);

            string json =
                await response.Content.ReadAsStringAsync();

            if (ensureSuccess && !response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    string.IsNullOrWhiteSpace(json)
                        ? response.ReasonPhrase
                        : json);
            }
        }
    }
}