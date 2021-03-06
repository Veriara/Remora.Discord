//
//  IGuildMemberUpdate.cs
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
using System.Collections.Generic;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.Core;

namespace Remora.Discord.API.Abstractions.Gateway.Events
{
    /// <summary>
    /// Represents a user being updated in the guild.
    /// </summary>
    public interface IGuildMemberUpdate : IGatewayEvent
    {
        /// <summary>
        /// Gets the ID of the guild the member is in.
        /// </summary>
        Snowflake GuildID { get; }

        /// <summary>
        /// Gets the roles the user has.
        /// </summary>
        IReadOnlyList<Snowflake> Roles { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        IUser User { get; }

        /// <summary>
        /// Gets the user's nickname, if they have one set.
        /// </summary>
        Optional<string?> Nickname { get; }

        /// <summary>
        /// Gets the date when the user joined the guild.
        /// </summary>
        DateTimeOffset JoinedAt { get; }

        /// <summary>
        /// Gets the date when the user started boosting the guild.
        /// </summary>
        Optional<DateTimeOffset?> PremiumSince { get; }
    }
}
