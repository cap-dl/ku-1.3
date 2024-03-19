using System.Text.Json.Serialization;

namespace PersonsApi.Models
{
    /* Recommendation: use Model suffix 
     * to distinguish from core entities easily
     */
    public class PersonModel
    {
        [JsonPropertyName("id")]
        public int PersonId { get; set; }

        [JsonPropertyName("f")]
        public string FirstName { get; set; }

        [JsonPropertyName("l")]
        public string LastName { get; set; }
    }
}
