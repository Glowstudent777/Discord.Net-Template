FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /Bot

# Copy the entire project directory
COPY . /Bot

# Restore project dependencies
RUN dotnet restore

# Build the project
RUN dotnet build --no-restore

# Apply Entity Framework Core migrations
RUN dotnet ef database update --no-build

# Run the project
CMD ["dotnet", "run", "--no-build", "--project", "Bot"]
