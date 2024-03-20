using System.Text.Json.Serialization;

namespace ApiShared.Models
{
    public class RefResultModel<T>
        : ResultModelBase
    {
        [JsonPropertyName("v")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Value { get; set; }
    }
}
