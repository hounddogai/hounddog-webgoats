FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClassifiedDocumentPortal.BlazorUi/ClassifiedDocumentPortal.BlazorUi.csproj", "ClassifiedDocumentPortal.BlazorUi/"]
COPY ["ClassifiedDocumentPortal.Application/ClassifiedDocumentPortal.Application.csproj", "ClassifiedDocumentPortal.Application/"]
COPY ["ClassifiedDocumentPortal.Domain/ClassifiedDocumentPortal.Domain.csproj", "ClassifiedDocumentPortal.Domain/"]
COPY ["ClassifiedDocumentPortal.Infrastructure/ClassifiedDocumentPortal.Infrastructure.csproj", "ClassifiedDocumentPortal.Infrastructure/"]
RUN dotnet restore "./ClassifiedDocumentPortal.BlazorUi/./ClassifiedDocumentPortal.BlazorUi.csproj"
COPY . .
WORKDIR "/src/ClassifiedDocumentPortal.BlazorUi"
RUN dotnet build "./ClassifiedDocumentPortal.BlazorUi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClassifiedDocumentPortal.BlazorUi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClassifiedDocumentPortal.BlazorUi.dll"]