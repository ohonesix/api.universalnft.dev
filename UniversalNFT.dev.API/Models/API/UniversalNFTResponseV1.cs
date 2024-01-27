using Swashbuckle.AspNetCore.Annotations;

namespace UniversalNFT.dev.API.Models.API
{
    public class UniversalNFTResponseV1
    {
        [SwaggerSchema("The NFTokenID as stored on the XRPL", Nullable = false)]
        public string NFTokenID { get; set; }

        [SwaggerSchema("The wallet address this NFToken is associated with as stored on the XRPL", Nullable = false)]
        public string OwnerAccount { get; set; }

        [SwaggerSchema("The extracted direct full-resolution image url", Nullable = false)]
        public string ImageUrl { get; set; }

        [SwaggerSchema("The cached thumbnail direct image url", Nullable = false)]
        public string ImageThumbnailCacheUrl { get; set; }

        [SwaggerSchema("The date and time in ISO format this request was last generated on the server e.g. 2023-05-20T15:30:00.0000000+00:00", Nullable = false)]
        public string Timestamp { get; set; }
    }
}
