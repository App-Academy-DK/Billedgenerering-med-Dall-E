using System.Text.Json.Serialization;

namespace DallEService
{

    public class ImageGenerationRequest
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; } = "dall-e-2"; // Default value

        [JsonPropertyName("n")]
        public int? NumberOfImages { get; set; } = 1; // Default value

        [JsonPropertyName("quality")]
        public string Quality { get; set; } = "standard"; // Default value

        [JsonPropertyName("response_format")]
        public string ResponseFormat { get; set; } = "url"; // Default value

        [JsonPropertyName("size")]
        public string Size { get; set; } = "1024x1024"; // Default value

        [JsonPropertyName("style")]
        public string Style { get; set; } = "vivid"; // Default value

        [JsonPropertyName("user")]
        public string User { get; set; } = "";
    }
}