ApiProject=--project ./src/App.Api/App.Api.csproj
InfraProject=--startup-project ./src/Infra/Infra.csproj

app-run:
	dotnet run ${ApiProject} & cd ui && ng serve -o
app-build:
	dotnet build eTournaments.sln
app-compose:
	docker compose up -d --build

db-create:
	dotnet ef ${InfraProject} migrations add DropkickDb -o Postgres/Migrations/History
db-drop:
	dotnet ef ${InfraProject} database drop
db-update:
	dotnet ef ${InfraProject} database update -- --environment Development
db-script:
	dotnet ef ${InfraProject} migrations script -- --environment Development


docker-build:
	make docker-build-api
	make docker-build-ui
docker-build-api:
	docker build -f docker/dockerfile_api --tag etournaments-api:beta .
docker-build-ui:
	docker build -f docker/dockerfile_ui --build-arg ENVIRONMENT=docker --tag etournaments-ui:beta .

ef-update:
	dotnet tool update --global dotnet-ef