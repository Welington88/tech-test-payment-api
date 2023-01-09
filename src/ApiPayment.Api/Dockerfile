#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ApiPayment.Api/ApiPayment.Api.csproj", "src/ApiPayment.Api/"]
COPY ["src/ApiPayment.CC.Ioc/ApiPayment.CC.Ioc.csproj", "src/ApiPayment.CC.Ioc/"]
COPY ["src/ApiPayment.Application/ApiPayment.Application.csproj", "src/ApiPayment.Application/"]
COPY ["src/ApiPayment.CC.Dto/ApiPayment.CC.Dto.csproj", "src/ApiPayment.CC.Dto/"]
COPY ["src/ApiPayment.Domain/ApiPayment.Domain.csproj", "src/ApiPayment.Domain/"]
COPY ["src/ApiPayment.Data/ApiPayment.Data.csproj", "src/ApiPayment.Data/"]
COPY ["src/ApiPayment.Api/DataBase/api.db", "src/ApiPayment.Api/DataBase/"]
RUN dotnet restore "src/ApiPayment.Api/ApiPayment.Api.csproj"
COPY . .
WORKDIR "/src/src/ApiPayment.Api"
RUN dotnet build "ApiPayment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiPayment.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiPayment.Api.dll"]
