@echo off

if "%1%" EQU "--build" (goto build) else (goto restart)

:build
docker compose -p dotnet_with_react_separate_development -f docker-compose.yml -f docker-compose.Development.yml --env-file Development.env up --build

goto end

:restart
docker compose -p dotnet_with_react_separate_development -f docker-compose.yml -f docker-compose.Development.yml --env-file Development.env up

:end