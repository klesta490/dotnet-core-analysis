@echo off

SET tag=latest
IF NOT "%1"=="" (
    SET tag=%1
)

dotnet publish ..\Exceptions.csproj -f netcoreapp2.1 --force -c Release -o bin\output
docker build -f DockerFile ..\bin\output -t quadient.azurecr.io/cloud/exceptions:%tag% 
