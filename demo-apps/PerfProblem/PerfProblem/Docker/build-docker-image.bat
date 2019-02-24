@echo off

SET tag=latest
IF NOT "%1"=="" (
    SET tag=%1
)

dotnet publish ..\PerfProblem.csproj -f netcoreapp2.1 --force -c Release -o bin\output
docker build -f DockerFile ..\bin\output -t quadient.azurecr.io/cloud/perf-problem:%tag% 
