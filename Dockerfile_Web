FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app
COPY . .
RUN ls
RUN dotnet restore ./Capstone.Web/Capstone.Web.csproj
RUN dotnet publish ./Capstone.Web/Capstone.Web.csproj -c Release -o web

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
EXPOSE 8080
WORKDIR /api
COPY --from=build /app/web .
RUN ls
CMD ["./Capstone.Web"]