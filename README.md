# eTournaments

[![build](https://github.com/morilon/fwks/actions/workflows/main.yml/badge.svg)](https://github.com/morilon/fwks/actions/workflows/main.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/L4L1JXUQN)

## Important Note

App used to kickstart new applications and stuff. A few practices that I consider good and use. 

> **Note**
> This is a work in progress so lots of things will change, docs are missing and things might not work.

## Software Requirements

### Required

- [Visual Studio 2022 Latest](https://visualstudio.microsoft.com/)
- [.NET Core SDK 7](https://dotnet.microsoft.com/download/dotnet-core)
- [Chocolatey](https://chocolatey.org/)
  - `choco install make`
- [Node.js](https://nodejs.org/en/)
  - `npm i @angular/cli -g`

### Optional

- [Solution Error Visualizer](https://marketplace.visualstudio.com/items?itemName=VisualStudioPlatformTeam.SolutionErrorVisualizer2022)
- NPM Packages
  - `npm i pnpm -g`

## Before running either locally or within docker

1. Create a user on [keycloak development realm](http://localhost:9999/auth/admin/master/console/#/development/users).
2. Install or update dotnet ef tools, run `make ef-update`
3. Execute `make db-update` to run migrations.

## Running Locally

To start the apps, run `make app-run`

## Running on Docker

To start the apps using docker, run `make app-compose`

## Environments endpoints

- Development [UI](http://localhost:4200) / [Api](https://localhost:5001/swagger)
- Docker [UI](http://localhost:25000) / [Api](https://localhost:25001/swagger)
- Live [UI](WIP) / [Api](WIP)
