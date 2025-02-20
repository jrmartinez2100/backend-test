# Usa el SDK adecuado
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos de la soluci�n
COPY backend-test.sln ./
COPY Application/*.csproj Application/
COPY Domain/*.csproj Domain/
COPY Infrastructure/*.csproj Infrastructure/
COPY Presentation/*.csproj Presentation/
COPY TestBackend/*.csproj TestBackend/
# Restaurar dependencias antes de copiar el resto del c�digo
RUN dotnet restore

# Copiar el resto del c�digo fuente
COPY . .

# Ejecutar pruebas antes de compilar la aplicación
WORKDIR /app/TestBackend
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov

# Cambiar a la carpeta del proyecto principal
WORKDIR /app/Presentation

# Compilar la aplicaci�n
RUN dotnet build

# Publicar la aplicaci�n
RUN dotnet publish -c Release -o /out

# Imagen final para la ejecuci�n
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /out .

# Ejecutar la aplicaci�n
ENTRYPOINT ["dotnet", "Presentation.dll"]
