# Stage 1: Build
FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0-jammy as builder
WORKDIR /src

# Copy project file first for caching
COPY ["OrderProcessingService/OrderProcessingService.csproj", "./"]

# 🔥 Remove stale artifacts before restoring
RUN rm -rf /src/bin /src/obj

# Copy all remaining source files
COPY . .

WORKDIR /workspace

USER vscode
WORKDIR /workspace

# Expose ports
EXPOSE 8080
