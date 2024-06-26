﻿using UniversalNFT.dev.API.Models.DTO;

namespace UniversalNFT.dev.API.Services.XRPL
{
    public interface IXRPLService
    {
        Task<IEnumerable<RippledAccountNFToken>> GetAllNFTs(string ownerAccount);

        Task<RippledAccountNFToken?> GetNFT(string tokenID, string ownerAccount);
    }
}