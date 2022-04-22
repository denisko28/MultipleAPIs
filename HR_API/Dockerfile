FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HR_API/HR_API.csproj", "HR_API/"]
RUN dotnet restore "HR_API/HR_API.csproj"
COPY . .
WORKDIR "/src/HR_API"
RUN dotnet build "HR_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR_API.dll"]
