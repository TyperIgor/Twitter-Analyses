#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TwitterAnalysis.Host/TwitterAnalysis.Host.csproj", "TwitterAnalysis.Host/"]
COPY ["TwitterAnalysis.Infrastructure.DI/TwitterAnalysis.Infrastructure.DI.csproj", "TwitterAnalysis.Infrastructure.DI/"]
COPY ["TwitterAnalysis.Application.Validations/TwitterAnalysis.Application.Validations.csproj", "TwitterAnalysis.Application.Validations/"]
COPY ["TwitterAnalysis.Application.Messages/TwitterAnalysis.Application.Messages.csproj", "TwitterAnalysis.Application.Messages/"]
COPY ["TwitterAnalysis.App.Service.Common/TwitterAnalysis.App.Service.Common.csproj", "TwitterAnalysis.App.Service.Common/"]
COPY ["TwitterAnalysis.App.Services/TwitterAnalysis.App.Services.csproj", "TwitterAnalysis.App.Services/"]
COPY ["TwitterAnalysis.Infrastructure.Data/TwitterAnalysis.Infrastructure.Data.csproj", "TwitterAnalysis.Infrastructure.Data/"]
COPY ["TwitterAnalysis.App.Service.Model/TwitterAnalysis.App.Service.Model.csproj", "TwitterAnalysis.App.Service.Model/"]
COPY ["TwitterAnalysis.Infrastructure.Service.Gateway/TwitterAnalysis.Infrastructure.Service.Gateway.csproj", "TwitterAnalysis.Infrastructure.Service.Gateway/"]
COPY ["TwitterAnalysis.Application.Services/TwitterAnalysis.Application.Services.csproj", "TwitterAnalysis.Application.Services/"]
COPY ["TwitterAnalysis.Application.Mapper/TwitterAnalysis.Application.Mapper.csproj", "TwitterAnalysis.Application.Mapper/"]
COPY ["TwitterAnalysis.Application.Middleware/TwitterAnalysis.Application.Middleware.csproj", "TwitterAnalysis.Application.Middleware/"]
RUN dotnet restore "TwitterAnalysis.Host/TwitterAnalysis.Host.csproj"
COPY . .
WORKDIR "/src/TwitterAnalysis.Host"
RUN dotnet build "TwitterAnalysis.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TwitterAnalysis.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "TwitterAnalysis.Host.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TwitterAnalysis.Host.dll