# Ejemplo Docker File para mas detalles consultar 
# https://docs.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY bin/Release/net6.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]