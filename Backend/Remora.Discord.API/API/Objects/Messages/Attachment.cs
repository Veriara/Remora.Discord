//
//  Attachment.cs
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
    public class Attachment : IAttachment
    {
        /// <inheritdoc />
        public Snowflake ID { get; }

        /// <inheritdoc />
        public string Filename { get; }

        /// <inheritdoc />
        public int Size { get; }

        /// <inheritdoc />
        public string Url { get; }

        /// <inheritdoc />
        public string ProxyUrl { get; }

        /// <inheritdoc />
        public int? Height { get; }

        /// <inheritdoc />
        public int? Width { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment"/> class.
        /// </summary>
        /// <param name="id">The ID of the attachment.</param>
        /// <param name="filename">The filename of the attachment.</param>
        /// <param name="size">The size of the attachment.</param>
        /// <param name="url">The URL of the attachment.</param>
        /// <param name="proxyUrl">The proxied URL of the attachment.</param>
        /// <param name="height">The height of the attachment, if it is an image.</param>
        /// <param name="width">The width of the attachment, if it is an image.</param>
        public Attachment(Snowflake id, string filename, int size, string url, string proxyUrl, int? height, int? width)
        {
            this.ID = id;
            this.Filename = filename;
            this.Size = size;
            this.Url = url;
            this.ProxyUrl = proxyUrl;
            this.Height = height;
            this.Width = width;
        }
    }
}
