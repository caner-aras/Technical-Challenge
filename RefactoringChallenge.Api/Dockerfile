#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["RefactoringChallenge.Api/RefactoringChallenge.csproj", "RefactoringChallenge.Api/"]
COPY ["RefactoringChallenge.Core/RefactoringChallenge.Core.csproj", "RefactoringChallenge.Core/"]
RUN dotnet restore "RefactoringChallenge.Api/RefactoringChallenge.csproj"
COPY . .
WORKDIR "/src/RefactoringChallenge.Api"
RUN dotnet build "RefactoringChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RefactoringChallenge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RefactoringChallenge.dll"]