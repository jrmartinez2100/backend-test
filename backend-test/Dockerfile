# Usa el SDK adecuado
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos de la solución
COPY backend-test.sln ./
COPY Application/*.csproj Application/
COPY Domain/*.csproj Domain/
COPY Infrastructure/*.csproj Infrastructure/
COPY Presentation/*.csproj Presentation/

# Restaurar dependencias antes de copiar el resto del código
RUN dotnet restore

# Copiar el resto del código fuente
COPY . .

# Cambiar a la carpeta del proyecto principal
WORKDIR /app/Presentation

# Compilar la aplicación
RUN dotnet build

# Publicar la aplicación
RUN dotnet publish -c Release -o /out

# Imagen final para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /out .

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "Presentation.dll"]
