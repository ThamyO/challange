FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["WebApiTakeUser/WebApiTakeUser.csproj", "WebApiTakeUser/"]
RUN dotnet restore "WebApiTakeUser/WebApiTakeUser.csproj"
COPY ./WebApiTakeUser ./WebApiTakeUser
WORKDIR "/src/WebApiTakeUser"
RUN dotnet build "WebApiTakeUser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiTakeUser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet WebApiTakeUser.dll