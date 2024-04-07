@echo off

if "%1%" EQU "--build" (goto build) else (goto restart)

:build
docker compose -p e_uni_manager_development -f docker-compose.yml -f docker-compose.Development.yml --env-file Development.env up --build

goto end

:restart
docker compose -p e_uni_manager_development -f docker-compose.yml -f docker-compose.Development.yml --env-file Development.env up

:end