# Etapa 1: Compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todos los archivos y restaurar
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Buscar automáticamente el archivo DLL correspondiente al proyecto
ENTRYPOINT ["sh", "-c", "exec dotnet $(ls *.deps.json | sed 's/.deps.json/.dll/')"]