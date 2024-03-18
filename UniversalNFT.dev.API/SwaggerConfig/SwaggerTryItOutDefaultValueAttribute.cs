namespace UniversalNFT.dev.API.SwaggerConfig
{
    public class SwaggerTryItOutDefaultValueAttribute : Attribute
    {
        public string Value { get; }

        public SwaggerTryItOutDefaultValueAttribute(string value)
        {
            Value = value;
        }
    }
}
