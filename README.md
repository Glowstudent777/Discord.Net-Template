# Houston
This is a Discord bot template made in C# using .Net 7

## Cloning

First clone this repo using the following command:
```sh
git clone https://github.com/Glowstudent777/Houston.git
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

## Contributing & Licensing

Contributions are welcome. Please read the full conditions surrounding contributing in the [LICENSE](LICENSE) document before sending in your contributions.
