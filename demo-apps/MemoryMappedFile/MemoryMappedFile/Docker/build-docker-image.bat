@echo off

SET tag=latest
IF NOT "%1"=="" (
    SET tag=%1
)

dotnet publish ..\MemoryMappedFile.csproj -f netcoreapp3.0 --force -c Release -o bin/output
docker build -f DockerFile ../bin/output -t quadient.azurecr.io/cloud/memorymap:%tag%
