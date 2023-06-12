# Discord.Net Template

This is a Discord bot made in C# using .Net 7.

# Cloning

Just a simple command in the terminal of your choice

```
git clone https://github.com/Glowstudent777/Discord.Net-Template.git
```

# Database

You'll need to fire up a MySQL or MariaDB database and grab it's connection details.

If your database isn't running locally you'll need to replace `localhost` with the server ip/hostname.

| Key      | Input                             |
| -------- | --------------------------------- |
| server   | URL or IP                         |
| port     | Usually 3306                      |
| uid      | Database username                 |
| pwd      | Database password                 |
| database | database you're using for the bot |

```
server=localhost;port=3306;uid=root;pwd=root;database=test_db
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

| Key      | Required | Default |
| -------- | -------- | ------- |
| TOKEN    | True     | None    |
| DATABASE | True     | None    |
