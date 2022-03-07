#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NotaryOnline.Web/NotaryOnline.Web.csproj", "NotaryOnline.Web/"]
COPY ["NotaryOnline.Api.ServiceModel/NotaryOnline.Api.ServiceModel.csproj", "NotaryOnline.Api.ServiceModel/"]
COPY ["SharedLib/SharedLib.csproj", "SharedLib/"]
RUN dotnet restore "NotaryOnline.Web/NotaryOnline.Web.csproj"
COPY . .
WORKDIR "/src/NotaryOnline.Web"
RUN dotnet build "NotaryOnline.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotaryOnline.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotaryOnline.Web.dll"]