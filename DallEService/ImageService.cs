using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static DallEService.ImageGenerationResponse;

namespace DallEService
{
    public class ImageService
    {
        private readonly string token;

        public ImageService(string token)
        {
            this.token = token;
        }
        public async Task<String?> GenerateImageAsync(string prompt, string size = "1024x1024")
        {
            var url = "https://api.openai.com/v1/images/generations";

            var requestBody = new ImageGenerationRequest
            {
                Prompt = prompt,
                Model = "dall-e-3",
                NumberOfImages = 1,
                Size = size
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            client.Timeout = TimeSpan.FromSeconds(600);

            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            var imageGenerationResponse = JsonSerializer.Deserialize<ImageGenerationResponse>(responseString);

            if (imageGenerationResponse != null && imageGenerationResponse.Data != null)
            {
                return imageGenerationResponse.Data[0].Url;
            }
            return null;
        }

        public string[] GenerateVariations(string imageFilepPath, string size = "1024x1024", int numberOfImages = 1, string responseFormat="url")
        {
            var url = "https://api.openai.com/v1/images/variations";

            var image = new FileStream(imageFilepPath, FileMode.Open, FileAccess.Read);
            byte[] imageData = File.ReadAllBytes(imageFilepPath);

            var form = new MultipartFormDataContent();           

            var byteArrayContent = new ByteArrayContent(imageData);
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
            form.Add(byteArrayContent, "image", Path.GetFileName(imageFilepPath));

            form.Add(new StringContent(size), "size");
            form.Add(new StringContent(numberOfImages.ToString()), "n");            
            form.Add(new StringContent(responseFormat), "response_format");

            HttpClient client = new();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            client.Timeout = TimeSpan.FromSeconds(600);
            var response = client.PostAsync(url, form).Result;
            
            var responseString = response.Content.ReadAsStringAsync().Result;
            var serviceResponse = JsonSerializer.Deserialize<ImageGenerationResponse>(responseString);

            if (responseFormat == "url")
            {
                return serviceResponse.Data.Select(data => data.Url).ToArray();
            } else
            {
                return serviceResponse.Data.Select(data => data.Base64Json).ToArray();
            }
            

        }

    }
}
