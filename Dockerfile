FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 8153

ENV ASPNETCORE_URLS=http://+:8153

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Ta9-Assignment.csproj", "./"]
RUN dotnet restore "Ta9-Assignment.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Ta9-Assignment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ta9-Assignment.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ta9-Assignment.dll"]


# docker run -v /host/path/to/certs:/container/path/to/certs -d ta9assignment "update-ca-certificates"
# docker run -d -p 8080:80 --name myapp aspnetapp
