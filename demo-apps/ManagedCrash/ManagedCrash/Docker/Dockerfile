FROM microsoft/dotnet:2.2.2-runtime-stretch-slim

WORKDIR /app

COPY . .

RUN apt-get -y update ;\ 
    apt-get -y install lldb-4.0 ;
RUN set -ex; \
    addgroup --gid 1001 "cloud"; \
    adduser --no-create-home "cloud" --gecos "" --disabled-password --gid 1001 --uid 1001; \
    chown -R "cloud:cloud" .; \    
    chmod +x /app/entrypoint.sh; \
	chmod +x /app/ManagedCrash.dll; 




ENTRYPOINT ["/app/entrypoint.sh"]
