@echo off
docker compose -p dotnet_with_react_separate_development -f docker-compose.yml -f docker-compose.Development.yml --env-file Development.env down