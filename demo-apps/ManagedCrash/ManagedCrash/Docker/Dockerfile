FROM microsoft/dotnet:2.2.2-runtime-stretch-slim

WORKDIR /app

COPY . .

RUN apt-get -y update ;\ 
    apt-get -y install lldb-4.0 ;

ENTRYPOINT ["/app/entrypoint.sh"]
