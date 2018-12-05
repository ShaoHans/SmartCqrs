#docker build -t smartcqrs:1.0 -f Services/SmartCqrs.API/Dockerfile --build-arg environment=Development .
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
ARG environment

ENV ASPNETCORE_ENVIRONMENT=$environment
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
#WORKDIR /app
EXPOSE 8800

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
#COPY ["BuildingBlocks/SmartCqrs.Infrastructure/SmartCqrs.Infrastructure.csproj", "BuildingBlocks/SmartCqrs.Infrastructure/"]
#COPY ["BuildingBlocks/SmartCqrs.Enumeration/SmartCqrs.Enumeration.csproj", "BuildingBlocks/SmartCqrs.Enumeration/"]
#COPY ["Commands/SmartCqrs.Application/SmartCqrs.Application.csproj", "Commands/SmartCqrs.Application/"]
#COPY ["Commands/SmartCqrs.Domain/SmartCqrs.Domain.csproj", "Commands/SmartCqrs.Domain/"]
#COPY ["Commands/SmartCqrs.Repository/SmartCqrs.Repository.csproj", "Commands/SmartCqrs.Repository/"]
#COPY ["Queries/SmartCqrs.Query/SmartCqrs.Query.csproj", "Queries/SmartCqrs.Query/"]
#COPY ["Services/SmartCqrs.API/SmartCqrs.API.csproj", "Services/SmartCqrs.API/"]
#RUN dotnet restore "Services/SmartCqrs.API/SmartCqrs.API.csproj"
COPY . .
WORKDIR /src/Services/SmartCqrs.API
RUN dotnet restore "SmartCqrs.API.csproj"
RUN dotnet build "SmartCqrs.API.csproj" 

FROM build AS publish
RUN dotnet publish "SmartCqrs.API.csproj" -o /output

FROM base AS final
WORKDIR /app
COPY --from=publish /output .
ENTRYPOINT ["dotnet", "SmartCqrs.API.dll"]