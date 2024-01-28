using System.Text.Json.Serialization;

namespace UniversalNFT.dev.API.Models.DTO
{
    public class RippledAccountNFTsResponse
    {
        [JsonPropertyName("result")]
        public RippledAccountNFTsResult? Result { get; set; }
    }

    public class RippledAccountNFTsResult
    {
        [JsonPropertyName("account_nfts")]
        public RippledAccountNFToken[]? NFTs { get; set; }

        [JsonPropertyName("marker")]
        public string? Marker { get; set; }
    }

    public class RippledAccountNFToken
    {
        public string? Issuer { get; set; }
        public string? NFTokenID { get; set; }
        public int NFTokenTaxon { get; set; }
        public string? URI { get; set; }

        [JsonPropertyName("nft_serial")]
        public int Serial { get; set; }
    }
}
