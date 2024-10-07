# Asegúrese de que el clúster de Service Fabric que implementa admita la imagen de contenedor que usa. Para obtener información sobre la compatibilidad de versiones de contenedor de Windows, consulte https://aka.ms/containerimagehelp.
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "PersonCatalog.dll"]
