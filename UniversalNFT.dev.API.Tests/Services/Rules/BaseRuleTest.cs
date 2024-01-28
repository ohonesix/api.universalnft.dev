using NSubstitute;
using UniversalNFT.dev.API.Facades;
using UniversalNFT.dev.API.Models.DTO;
using UniversalNFT.dev.API.Services.Providers;
using UniversalNFT.dev.API.Services.Rules;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class BaseRuleTest
    {
        public required IRulesEngine _classUnderTest;
        public required IOnXRPService _mockOnXRPService;
        public required IHttpFacade _mockHttpFacade;

        public required RippledAccountNFToken Token;

        [SetUp]
        public void Setup()
        {
            _mockOnXRPService = Substitute.For<IOnXRPService>();
            _mockHttpFacade = Substitute.For<IHttpFacade>();

            _classUnderTest = new RulesEngine(_mockOnXRPService, _mockHttpFacade);

            Token = new RippledAccountNFToken
            {
                NFTokenID = TestConstants.TokenId,
                Serial = TestConstants.Serial,
                Issuer = TestConstants.TokenIssuer,
                NFTokenTaxon = TestConstants.Taxon
            };
        }
    }
}
