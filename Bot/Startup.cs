using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bot
{
    public class Startup
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private DiscordShardedClient _client;
        private InteractionService _interactions;
        private CommandService _commands;
        private IServiceProvider _services;

        private System.Collections.Generic.IEnumerable<Discord.Interactions.ModuleInfo> _interactionModules;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public async Task Initialize()
        {
            var clientConfig = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All,
                LogLevel = LogSeverity.Info
            };

            await using var services = ConfigureServices(clientConfig);
            _client = services.GetRequiredService<DiscordShardedClient>();

            _client.Log += LogAsync;
            services.GetRequiredService<CommandService>().Log += LogAsync;

            var interactions = services.GetRequiredService<InteractionService>();
            _interactions = interactions;

            InstallCommands();

            // Tokens should be considered secret data and never hard-coded.
            // We can read from the environment variable to avoid hardcoding.
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));

            await _client.SetGameAsync("NHL Games", null, ActivityType.Watching);
            await _client.StartAsync();

            // Here we initialize the logic required to register our commands.
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();
            services.GetRequiredService<InteractionHandlingService>().Initialize();
            services.GetRequiredService<MessageHandler>();

            await Task.Delay(-1);
        }

        public void InstallCommands()
        {
            _client.ShardReady += RegisterCommands;
        }

        private async Task RegisterCommands(DiscordSocketClient client)
        {
            if (client.ShardId != 0) return;
            _interactionModules = await _interactions.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private static Task LogAsync(LogMessage log)
        {
            if(log.Exception is GatewayReconnectException)
            {
                if (log.Exception.InnerException is not null)
                    Console.WriteLine(log.Exception.InnerException.Message);
                return Task.CompletedTask;
            }

            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private static ServiceProvider ConfigureServices(DiscordSocketConfig clientConfig)
        {
            return new ServiceCollection()
                .AddSingleton(new DiscordShardedClient(clientConfig))
                .AddSingleton<CommandService>()
                .AddSingleton<InteractionService>()
                .AddSingleton<InteractionHandlingService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<MessageHandler>()
                .BuildServiceProvider();
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}