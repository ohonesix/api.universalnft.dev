using Swashbuckle.AspNetCore.Annotations;

namespace UniversalNFT.dev.API.Models.API
{
    public class Artv0Response
    {
        [SwaggerSchema("Always \"ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU\"", Nullable = false)]
        public string schema { get => "ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"; }

        [SwaggerSchema("Always \"art.v0\"", Nullable = false)]
        public string nftType { get => "art.v0"; }

        [SwaggerSchema("The NFTokenID", Nullable = false)]
        public string name {  get; set; }

        [SwaggerSchema("The date and time in ISO format this request was last generated on the server e.g. 2023-05-20T15:30:00.0000000+00:00", Nullable = false)]
        public string description { get; set; }

        [SwaggerSchema("The extracted direct full-resolution image url", Nullable = false)]
        public string image { get; set; }
    }
}
