# Arguments
ARG PROJECT_NAME="EstiPro.Api"
ARG PROJECT_FILE="./src/$PROJECT_NAME/$PROJECT_NAME.csproj"
ARG MSBUILD_BUILDCONFIGURATION=Release
ARG DOTNET_SDK_IMAGE=mcr.microsoft.com/dotnet/sdk:9.0
ARG DOTNET_ASPNET_IMAGE=mcr.microsoft.com/dotnet/aspnet:9.0

# Base runtime image
FROM $DOTNET_ASPNET_IMAGE AS base
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
WORKDIR /app

# Build image
FROM $DOTNET_SDK_IMAGE AS build
WORKDIR /src

# Copy the entire solution directory, including Directory.Build.props
COPY ./ ./ 

# Restore dependencies
ARG PROJECT_FILE
RUN dotnet restore $PROJECT_FILE

# Build the project
ARG MSBUILD_BUILDCONFIGURATION
RUN dotnet build $PROJECT_FILE \
    --configuration $MSBUILD_BUILDCONFIGURATION \
    --no-restore

# Publish image
FROM build AS publish

ARG PROJECT_FILE
ARG MSBUILD_BUILDCONFIGURATION

# Publish artifacts
RUN dotnet publish $PROJECT_FILE \
    --configuration $MSBUILD_BUILDCONFIGURATION \
    --output /app \
    --no-build

# Final runtime image
FROM base AS final

COPY --from=publish /app .

# Set the entry point
ARG PROJECT_NAME
ENV DOTNET_ENTRYPOINT=$PROJECT_NAME.dll
ENTRYPOINT ["dotnet", "$DOTNET_ENTRYPOINT"]
