FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HotCode.Operation/HotCode.Operation.csproj", "HotCode.Operation/"]
RUN dotnet restore "HotCode.Operation/HotCode.Operation.csproj"
COPY . .
WORKDIR "/src/HotCode.Operation"
RUN dotnet build "HotCode.Operation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotCode.Operation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotCode.Operation.dll"]