FROM microsoft/aspnetcore-build AS builder
RUN curl -o- -L https://yarnpkg.com/install.sh | bash && export PATH="$HOME/.yarn/bin:$PATH"
WORKDIR ../..
COPY ./src/*/*.csproj .
RUN dotnet restore
WORKDIR .
RUN yarn && dotnet publish --output /app/ --configuration Release

FROM microsoft/aspnetcore
MAINTAINER ZA-PT
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "Obsidian.dll"]
