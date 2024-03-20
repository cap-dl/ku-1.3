using System.Text.Json.Serialization;

namespace ApiShared.Models
{
    public class ResultModel<T>
    {
        private List<ErrorModel>? errors = null;

        [JsonPropertyName("v")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Value { get; set; }


        [JsonPropertyName("e")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ErrorModel>? Errors
        {
            get => errors;
            set => errors = value != null && value.Count > 0
                ? value
                : null;                
        }
    }
}
