using System.Text.Json.Serialization;

namespace WebApi.Utils
{
    public interface IActionResultApi<T>
    {
        [JsonPropertyOrder(0)]
        public int StatusCode { get; }

        [JsonPropertyOrder(1)]
        public string Message { get; }
        
        [JsonPropertyOrder(2)]
        public T? Data { get; }
    }

    public interface IActionResultApi
    {
        [JsonPropertyOrder(0)]
        public int StatusCode { get; }

        [JsonPropertyOrder(1)]
        public string Message { get; }
    }
}
