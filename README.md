# Discord.Net Template

This is a Discord bot template made in C# using .Net 7

### Features

- [x] Dockerized
- [x] Database
- [x] Organized Layout
- [x] Nice Logs
- [ ] Cron Jobs

## Cloning

Just a simple command in the terminal of your choice

```
git clone https://github.com/Glowstudent777/Discord.Net-Template.git
```

## Configuration

### Setting the Token
1. Get your Discord bot token from the [Discord Developer Portal](https://discord.com/developers/applications).
2. Rename `.env.example` to `.env` using the following command:
```sh
mv .env.example .env
```
3. Add your token to the `.env` file. This can be done using this command: `sudo nano .env`. Paste your token after `TOKEN=`. Lastly save the file using <kbd>Ctrl</kbd>+<kbd>X</kbd>, then <kbd>Y</kbd> and finally <kbd>Enter</kbd>.

## Docker

You will need to have Docker installed for this, if you don't have it set up, [here is a guide](https://docs.docker.com/engine/install/debian/#install-using-the-repository).

After you have installed and set up Docker run the following command:
```sh
docker compose up -d --build
```

# Development

I suggest using [Visual Studio](https://visualstudio.microsoft.com/) for this project.

## Changing Project Name

If you change the project name from `Houston` to something else make sure to update these files with your new name.
- [docker-compose.yml](docker-compose.yml)
- [src/Houston.sln](src/Houston.sln)
- [src/Houston.Bot/Dockerfile](src/Houston.Bot/Dockerfile)
- OPTIONAL if you want the GitHub Workflow, [.github/workflows/docker-image.yml](.github/workflows/docker-image.yml)

## Environment Variables

| Key      | Required | Default |
| -------- | -------- | ------- |
| TOKEN    | True     | None    |
| DATABASE | True     | None    |

If you don't know how, here is how you enter environment variables in Visual Studio for local testing.

1. Right click the `Xxx.Bot` project and select `Properties`
2. In the menu on the right side, go to `Debug`
3. Select `Open debug launch profiles UI`
4. Add `TOKEN` and `DATABASE` to the `Environment variables` section

## Database

You'll need to fire up a [Postgresql](https://www.postgresql.org/) database and grab it's connection details.

If your database isn't running locally you'll need to replace `localhost` with the server ip/hostname.

| Key      	  | Input                             |
| ----------- | --------------------------------- |
| Server   	  | IP or hostname                    |
| Port     	  | Usually 5432                      |
| UserId      | Database username                 |
| Password    | Database password                 |
| Database 	  | database you're using for the bot |

```
Server=localhost;Port=5432;UserId=admin;Password=securepassword;Database=superdb
```

## Building

Build the bot and restore the dependancies using the `dotnet` command.

```
dotnet restore && dotnet build
```

## Running the Bot

In Visual Studio you'll find two buttons at the top; a solid green start button with the text `Xxx.Bot` and one next to it that has a green outline without text. The solid green with text is for debugging and the one without starts without debugging.


# Contributing & Licensing

Contributions are welcome. Please read the full conditions surrounding contributing in the [LICENSE](LICENSE) document before sending in your contributions.
