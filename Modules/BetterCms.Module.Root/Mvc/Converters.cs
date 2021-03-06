﻿using System;

namespace BetterCms.Module.Root.Mvc
{
    /// <summary>
    /// Converter helpers.
    /// </summary>
    public static class Converters
    {
        /// <summary>
        /// Converts string source to the value of Guid type or Guid.Empty.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>A value or Guid.Empty.</returns>
        public static Guid ToGuidOrDefault(this string source)
        {
            Guid result;
            if (Guid.TryParse(source, out result))
            {
                return result;
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Determines whether the given Guid has default value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///   <c>true</c> if the specified source has default value; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasDefaultValue(this Guid source)
        {
            return source == default(Guid);
        }

        /// <summary>
        /// Converts string source to the value on integer type or zero.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A value or default integer value (zero).</returns>
        public static int ToIntOrDefault(this string source)
        {
            int result;
            if (int.TryParse(source, out result))
            {
                return result;
            }
            return default(int);
        }
    }
}