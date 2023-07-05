using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Bot
{
    public class Startup
    {
        private DiscordShardedClient _client;
        private InteractionService _interactions;
        private CommandService _commands;
        private IServiceProvider _services;

        private System.Collections.Generic.IEnumerable<Discord.Interactions.ModuleInfo> _interactionModules;

        public async Task Initialize()
        {

            var clientConfig = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All,
            };

            await using var services = ConfigureServices(clientConfig);
            _services = services.GetRequiredService<IServiceProvider>();
            _client = services.GetRequiredService<DiscordShardedClient>();

            var db = services.GetRequiredService<Database>();
            await db.Database.MigrateAsync();

            _client.Log += LogAsync;
            services.GetRequiredService<CommandService>().Log += LogAsync;

            var interactions = services.GetRequiredService<InteractionService>();
            _interactions = interactions;

            await InstallCommandsAsync();

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

        public async Task InstallCommandsAsync()
        {
            _client.ShardReady += RegisterCommands;

            var assembly = Assembly.GetEntryAssembly();
            _interactionModules = await _interactions.AddModulesAsync(assembly, _services);
        }

        private async Task RegisterCommands(DiscordSocketClient client)
        {
            if (client.ShardId != 0) return;
            await _interactions.AddModulesGloballyAsync(true, _interactionModules.ToArray());
        }

        private static Task LogAsync(LogMessage log)
        {
            if (log.Exception is GatewayReconnectException)
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
                .AddDbContextPool<Database>(options =>
                {
                    var connectionString = Environment.GetEnvironmentVariable("DATABASE");
                    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));
                })
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