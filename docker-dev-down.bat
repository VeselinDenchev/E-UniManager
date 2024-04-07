@echo off
docker compose -p e_uni_manager_development -f docker-compose.yml -f docker-compose.Development.yml --env-file Development.env down