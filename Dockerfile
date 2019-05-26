FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

ADD dist/ /app
WORKDIR /app

EXPOSE 5010

ENV ASPNETCORE_URLS=http://0.0.0.0:5010
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "VipsApi.dll"]
