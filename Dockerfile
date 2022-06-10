FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80

WORKDIR /Reflexobot.API/
COPY ["Reflexobot.API/Reflexobot.API.csproj", "/source/"]
WORKDIR /Reflexobot.Date/
COPY ["Reflexobot.Data/Reflexobot.Data.csproj", "/data/"]
WORKDIR /Reflexobot.Entities/
COPY ["Reflexobot.Entities/Reflexobot.Entities.csproj", "/entities/"]
WORKDIR /Reflexobot.Models/
COPY ["Reflexobot.Models/Reflexobot.Models.csproj", "/models/"]
WORKDIR /Reflexobot.Repositories/
COPY ["Reflexobot.Repositories/Reflexobot.Repositories.csproj", "/repositories/"]
WORKDIR /Reflexobot.Services/
COPY ["/Reflexobot.Services/Reflexobot.Services.csproj", "/services/"]
RUN dotnet restore "/source/Reflexobot.API.csproj"
COPY . .
WORKDIR /source/
RUN dotnet build "/source/Reflexobot.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/source/Reflexobot.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reflexobot.API.dll"]