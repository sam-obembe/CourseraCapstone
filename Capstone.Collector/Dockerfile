FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app
COPY . .
RUN ls
RUN dotnet restore ./Capstone.Collector/Capstone.Collector.csproj
RUN dotnet publish ./Capstone.Collector/Capstone.Collector.csproj -c Release -o collector

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
EXPOSE 8080
WORKDIR /api
COPY --from=build /app/collector .
RUN ls
CMD ["./Capstone.Collector"]