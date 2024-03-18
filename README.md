# api.universalnft.dev

Please see the full documentation here https://github.com/ohonesix/api.universalnft.dev/wiki

[![Build and Package](https://github.com/ohonesix/api.universalnft.dev/actions/workflows/main.yml/badge.svg)](https://github.com/ohonesix/api.universalnft.dev/actions/workflows/main.yml)


## Docker

Build using the Dockerfile from within `/UniversalNFT.dev.API/`

`docker build -t universalnftdev .`

Then run (uses port 5103 by default)

`docker run -d -p 5103:5103 --name universalnftdevapi universalnftdev`
