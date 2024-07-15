using DallEService;
using ImageVariation;

ImageService service = new(Token.token);

string[] base64images = service.GenerateVariations("C:\\Users\\ktlh\\Downloads\\vegetables.png", numberOfImages: 5, responseFormat: "b64_json");

foreach (var image in base64images)
{
    var bytes = Convert.FromBase64String(image);
    File.WriteAllBytes($"C:\\Users\\ktlh\\Downloads\\veg-{Guid.NewGuid()}.png", bytes);
}