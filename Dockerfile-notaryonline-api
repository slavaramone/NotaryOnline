#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NotaryOnline.Api/NotaryOnline.Api.csproj", "NotaryOnline.Api/"]
COPY ["NotaryOnline.Api.ServiceModel/NotaryOnline.Api.ServiceModel.csproj", "NotaryOnline.Api.ServiceModel/"]
COPY ["SharedLib/SharedLib.csproj", "SharedLib/"]
COPY ["NotaryOnline.Api.ServiceInterface/NotaryOnline.Api.ServiceInterface.csproj", "NotaryOnline.Api.ServiceInterface/"]
COPY ["NotaryOnline.DataAccess/NotaryOnline.DataAccess.csproj", "NotaryOnline.DataAccess/"]
COPY ["NotaryOnline.Entities/NotaryOnline.Entities.csproj", "NotaryOnline.Entities/"]
RUN dotnet restore "NotaryOnline.Api/NotaryOnline.Api.csproj"
COPY . .
WORKDIR "/src/NotaryOnline.Api"
RUN dotnet build "NotaryOnline.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotaryOnline.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotaryOnline.Api.dll"]