using System.Text.Json;

namespace UniversalNFT.dev.API.Services.Images
{
    public static class JsonImageExtractor
    {
        public static string ExtractImageUrl(string jsonString)
        {
            string[] possiblePropertyNames = ["image", "image_url", "imageurl", "picture", "picture_url", "photo", "data", "url"];

            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                JsonElement root = doc.RootElement;

                foreach (string propertyName in possiblePropertyNames)
                {
                    // Check if any of the property names match in a case-insensitive manner
                    var property = root.EnumerateObject().FirstOrDefault(p =>
                        string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

                    // If a matching property has been found and is of the correct type, return its value
                    if (property.Value.ValueKind == JsonValueKind.String)
                    {
                        return property.Value.GetString();
                    }
                }
            }

            return null;
        }
    }
}
