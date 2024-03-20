using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ApiShared.Models
{
    public class ErrorModel
    {
        private Dictionary<string, object>? metadata;
        private List<ErrorModel>? reasons;

        [JsonPropertyName("m")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault
            | JsonIgnoreCondition.WhenWritingNull)]
        [DefaultValue("")]
        public string Message { get; set; } = string.Empty;


        [JsonPropertyName("d")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Metadata
        {
            get => metadata;
            set => metadata = value != null && value.Count > 0
                ? value
                : null;
        }


        [JsonPropertyName("r")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ErrorModel>? Reasons
        {
            get => reasons;
            set => reasons = value != null && value.Count > 0
                ? value
                : null;
        }
    }
}
