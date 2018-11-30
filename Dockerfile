FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
ARG environment

ENV ASPNETCORE_ENVIRONMENT=$environment
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
#WORKDIR /app
EXPOSE 8300

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CarDealerBang.API/CarDealerBang.API.csproj", "CarDealerBang.API/"]
COPY ["Command/CarDealerBang.Application/CarDealerBang.Application.csproj", "Command/CarDealerBang.Application/"]
COPY ["BuildingBlocks/CarDealerBang.Infrastructure/CarDealerBang.Infrastructure.csproj", "BuildingBlocks/CarDealerBang.Infrastructure/"]
COPY ["Command/CarDealerBang.Domain/CarDealerBang.Domain.csproj", "Command/CarDealerBang.Domain/"]
COPY ["BuildingBlocks/CarDealerBang.Enumeration/CarDealerBang.Enumeration.csproj", "BuildingBlocks/CarDealerBang.Enumeration/"]
COPY ["Query/CarDealerBang.Query/CarDealerBang.Query.csproj", "Query/CarDealerBang.Query/"]
COPY ["Command/CarDealerBang.Repository/CarDealerBang.Repository.csproj", "Command/CarDealerBang.Repository/"]
RUN dotnet restore "CarDealerBang.API/CarDealerBang.API.csproj"
COPY . .
WORKDIR "/src/CarDealerBang.API"
RUN dotnet build "CarDealerBang.API.csproj" 

FROM build AS publish
RUN dotnet publish "CarDealerBang.API.csproj" -o /output

FROM base AS final
WORKDIR /app
COPY --from=publish /output .
ENTRYPOINT ["dotnet", "CarDealerBang.API.dll"]