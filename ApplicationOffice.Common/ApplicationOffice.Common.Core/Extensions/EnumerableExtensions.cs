using System;
using System.Collections.Generic;

namespace ApplicationOffice.Common.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Iterate through sequence and apply <see cref="Action{T}" />> to each element
        /// </summary>
        /// <param name="enumerable">Elements for action</param>
        /// <param name="func">Action for applying to each element</param>
        /// <typeparam name="T">Any type</typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> func)
        {
            foreach (var item in enumerable)
            {
                func(item);
            }
        }
    }
}