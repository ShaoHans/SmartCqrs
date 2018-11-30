#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["Services/SmartCqrs.API/SmartCqrs.API.csproj", "Services/SmartCqrs.API/"]
RUN dotnet restore "Services/SmartCqrs.API/SmartCqrs.API.csproj"
COPY . .
WORKDIR "/src/Services/SmartCqrs.API"
RUN dotnet build "SmartCqrs.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartCqrs.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartCqrs.API.dll"]