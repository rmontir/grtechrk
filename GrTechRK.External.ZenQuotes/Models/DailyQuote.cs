using System.Text.Json.Serialization;

namespace GrTechRK.External.ZenQuotes.Models
{
    public class DailyQuote
    {
        [JsonPropertyName("q")]
        public string Quote { get; set; }

        [JsonPropertyName("a")]
        public string Author { get; set; }

        [JsonPropertyName("h")]
        public string HtmlText { get; set; }
    }
}
