@echo off

SET tag=latest
IF NOT "%1"=="" (
    SET tag=%1
)

docker push quadient.azurecr.io/cloud/exceptions:%tag%
