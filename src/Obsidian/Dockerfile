FROM microsoft/aspnetcore-build AS builder
WORKDIR .
RUN curl -o- -L https://yarnpkg.com/install.sh | bash
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN yarn && dotnet publish --output /app/ --configuration Release

FROM microsoft/aspnetcore
MAINTAINER ZA-PT
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "Obsidian.dll"]
