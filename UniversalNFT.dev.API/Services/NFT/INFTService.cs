﻿using UniversalNFT.dev.API.Models.API;

namespace UniversalNFT.dev.API.Services.NFT
{
    public interface INFTService
    {
        Task<UniversalNFTResponseV1> GetNFT(string NFTokenID, string OwnerWalletAddress);

        Task<Artv0Response> GetArtv0(string NFTokenID, string OwnerWalletAddress);

        Task<IEnumerable<UniversalNFTResponseV1>> GetAllNFTs(string OwnerWalletAddress);
    }
}