FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

ADD dist/ /app
WORKDIR /app

ENV PORT=5010
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "VipsApi.dll"]
