using UniversalNFT.dev.API.Models.DTO;

namespace UniversalNFT.dev.API.Services.Rules
{
    public interface IRulesEngine
    {
        Task<string?> ProcessNFToken(RippledAccountNFToken nfToken);
    }
}