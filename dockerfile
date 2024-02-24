FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY ./B50Api/bin/Release/net8.0/linux-x64 ./


ENTRYPOINT ["./B50Api"]