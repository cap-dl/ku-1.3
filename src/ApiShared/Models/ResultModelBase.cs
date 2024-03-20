using System.Text.Json.Serialization;

namespace ApiShared.Models
{
    public abstract class ResultModelBase
    {
        private List<ErrorModel>? errors = null;


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
