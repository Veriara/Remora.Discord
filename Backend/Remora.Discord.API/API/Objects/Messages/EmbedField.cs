//
//  EmbedField.cs
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

using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.Core;

namespace Remora.Discord.API.Objects
{
    /// <inheritdoc />
    public class EmbedField : IEmbedField
    {
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Value { get; }

        /// <inheritdoc />
        public Optional<bool> IsInline { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="value">The field value.</param>
        /// <param name="isInline">Whether the field is inline.</param>
        public EmbedField(string name, string value, Optional<bool> isInline)
        {
            this.Name = name;
            this.Value = value;
            this.IsInline = isInline;
        }
    }
}
