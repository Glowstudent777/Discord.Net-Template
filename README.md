# Discord.Net Template

This is a Discord bot made in C# using .Net 7.

# Cloning

Just a simple command in the terminal of your choice

```
git clone https://github.com/Phish-Net/Discord.Net-Template.git
```

# Building
Build the bot and restore the dependancies using the `dotnet` command.

```
dotnet restore && dotnet build
```

# Running the Bot

## Docker

This project is meant to run in a Docker container so make sure you don't forget to configure the variables in the `docker-compose.yml` file before running the bot.

```
docker-compose up -d
```

### Environment Variables

| Key            | Required | Default |
| -------------- | -------- | ------- |
| TOKEN          | True     | None    |
