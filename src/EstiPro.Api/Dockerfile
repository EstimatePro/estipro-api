ARG PROJECT_NAME="EstiPro.Api"
ARG ASSEMBLY_NAME="$PROJECT_NAME"
ARG PROJECT_FILE="./src/$PROJECT_NAME/$PROJECT_NAME.csproj"
ARG MSBUILD_BUILDCONFIGURATION=Release
ARG DOTNET_SDK_IMAGE=mcr.microsoft.com/dotnet/sdk:9.0
ARG DOTNET_ASPNET_IMAGE=mcr.microsoft.com/dotnet/aspnet:9.0

FROM $DOTNET_ASPNET_IMAGE AS base
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
WORKDIR /app

FROM $DOTNET_SDK_IMAGE AS build
WORKDIR /src

# Copy files for restore.
COPY ["./src/*/*.csproj", "./"]
RUN for file in $(ls *.csproj); do mkdir -p ./src/${file%.*}/ && mv $file ./src/${file%.*}/; done

ARG PROJECT_FILE
RUN dotnet restore $PROJECT_FILE

# Copy files for a build.
COPY ["./src", "./src"]

# Set the build configuration.
ARG MSBUILD_BUILDCONFIGURATION

# Build the project.
RUN dotnet build $PROJECT_FILE \
    --configuration $MSBUILD_BUILDCONFIGURATION \
    --no-restore

FROM build AS publish

ARG PROJECT_FILE
ARG MSBUILD_BUILDCONFIGURATION

# Publish artifacts.
RUN dotnet publish $PROJECT_FILE \
    --configuration $MSBUILD_BUILDCONFIGURATION \
    -o /app \
    --no-build

FROM base AS final

COPY --from=publish /app .

ARG ASSEMBLY_NAME
ENV DOTNET_ENTRYPOINT=$ASSEMBLY_NAME.dll
ENTRYPOINT ["sh", "-c", "dotnet $DOTNET_ENTRYPOINT"]