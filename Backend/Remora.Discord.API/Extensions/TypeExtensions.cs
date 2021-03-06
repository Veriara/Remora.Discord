//
//  TypeExtensions.cs
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
using System.Linq;
using System.Reflection;
using Remora.Discord.Core;

namespace Remora.Discord.API.Extensions
{
    /// <summary>
    /// Defines extension methods to the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the given type is a closed <see cref="Optional{TValue}"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the type is a closed Optional; otherwise, false.</returns>
        public static bool IsOptional(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Optional<>);
        }

        /// <summary>
        /// Determines whether the given type is a closed <see cref="Nullable{TValue}"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the type is a closed Nullable; otherwise, false.</returns>
        public static bool IsNullable(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Retrieves the innermost type from a type wrapped by
        /// <see cref="Nullable{T}"/> or <see cref="Optional{TValue}"/>.
        /// </summary>
        /// <param name="type">The type to unwrap.</param>
        /// <returns>The unwrapped type.</returns>
        public static Type Unwrap(this Type type)
        {
            var currentType = type;
            while (currentType.IsGenericType)
            {
                if (currentType.IsOptional() || currentType.IsNullable())
                {
                    currentType = currentType.GetGenericArguments()[0];
                    continue;
                }

                break;
            }

            return currentType;
        }

        /// <summary>
        /// Gets all publicly visible properties of the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The public properties.</returns>
        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            if (!type.IsInterface)
            {
                foreach (var property in type.GetProperties())
                {
                    if (property.DeclaringType != type && !(property.DeclaringType is null))
                    {
                        // this is an inherited property, so we'll return the declaring class type's version of it
                        yield return property.DeclaringType.GetProperty(property.Name) ?? throw new MissingMemberException();
                        continue;
                    }

                    yield return property;
                }

                yield break;
            }

            foreach (var implementedInterface in type.GetInterfaces().Concat(new[] { type }))
            {
                foreach (var property in implementedInterface.GetProperties())
                {
                    yield return property;
                }
            }
        }
    }
}
