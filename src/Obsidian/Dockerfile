MAINTAINER ZA-PT

FROM microsoft/aspnetcore-build AS builder
WORKDIR .

RUN curl -o- -L https://yarnpkg.com/install.sh | sudo bash

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish --output /app/ --configuration Release

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "Obsidian.dll"]
