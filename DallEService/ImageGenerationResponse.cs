using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DallEService
{

    public class ImageGenerationResponse
    {
        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("data")]
        public List<ImageData> Data { get; set; }

        public class ImageData
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("b64_json")]
            public string Base64Json { get; set; }
        }
    }
}

