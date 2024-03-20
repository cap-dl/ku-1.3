using System.Text.Json.Serialization;

namespace ApiShared.Models
{
    public class ValueResultModel<T>
        : ResultModelBase
    {
        [JsonPropertyName("v")]
        public T Value { get; set; }
    }
}
