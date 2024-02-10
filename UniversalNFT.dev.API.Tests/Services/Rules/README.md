# Testing Rules and Variations

For the first 1-49 projects we test them each individually as that is a large spread of the XRPL NFTs including checking we can get the metadata by using the actual Token.URI format.

For 50+ we don't check the Token.URI format and just validate the metadata json as its quite likely there won't be further variation.

Any reported variation in Token.URI would be worthy of its own test, if found.

For example 080-BirdjpgTest.cs is the first to just have the media url linked directly from the blockchain, so it has its own test.

## Test Runner

We combine tests into one file for 50+ to speed up testing. Use the format below to add more

[TestCase("Name Of Collection", "Expected returned image url from metadata", @"full metadata without linebreaks from ipfs")]