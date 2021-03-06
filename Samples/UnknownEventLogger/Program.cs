﻿//
//  Program.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Remora.Discord.Gateway;
using Remora.Discord.Gateway.Extensions;

namespace Remora.Samples.UnknownEventLogger
{
    /// <summary>
    /// Represents the main class of the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entrypoint of the program.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous program execution.</returns>
        public static async Task Main(string[] args)
        {
            var cancellationSource = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                cancellationSource.Cancel();
            };

            var botToken =
                Environment.GetEnvironmentVariable("REMORA_BOT_TOKEN")
                ?? throw new InvalidOperationException
                (
                    "No bot token has been provided. Set the REMORA_BOT_TOKEN environment variable to a valid token."
                );

            var services = new ServiceCollection()
                .AddLogging
                (
                    c => c
                        .AddConsole()
                        .AddFilter("System.Net.Http.HttpClient.Discord.LogicalHandler", LogLevel.Warning)
                        .AddFilter("System.Net.Http.HttpClient.Discord.ClientHandler", LogLevel.Warning)
                )
                .AddDiscordGateway(() => botToken)
                .BuildServiceProvider();

            var log = services.GetRequiredService<ILogger<Program>>();

            var gatewayClient = services.GetRequiredService<DiscordGatewayClient>();
            gatewayClient.SubscribeResponder<UnknownEventResponder>();

            var runResult = await gatewayClient.RunAsync(cancellationSource.Token);
            if (!runResult.IsSuccess)
            {
                log.LogError(runResult.Exception, runResult.ErrorReason);

                if (runResult.GatewayCloseStatus.HasValue)
                {
                    log.LogError($"Gateway close status: {runResult.GatewayCloseStatus}");
                }

                if (runResult.WebSocketCloseStatus.HasValue)
                {
                    log.LogError($"Websocket close status: {runResult.WebSocketCloseStatus}");
                }
            }

            log.LogInformation("Bye bye");
        }
    }
}
