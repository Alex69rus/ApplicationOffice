using System;
using System.Collections.Generic;

namespace ApplicationOffice.Common.Api.Cors
{
    internal static class PrivateExtensions
    {
        /// <summary>
        ///     Determines whether collection is empty using its Count property.
        /// </summary>
        /// <param name="collection">Source collection.</param>
        /// <typeparam name="TEntry">Collection entry type.</typeparam>
        /// <returns>Boolean value representing whether collection is empty.</returns>
        public static bool IsEmpty<TEntry>(this ICollection<TEntry> collection)
        {
            return collection.Count == 0;
        }

        /// <summary>
        ///     Applies custom callback to collection if it's empty.
        /// </summary>
        /// <param name="collection">Source collection.</param>
        /// <param name="callback">Custom callback to apply.</param>
        /// <typeparam name="TEntry">Collection entry type.</typeparam>
        /// <returns>Collections (which can be modified by callback if it was empty).</returns>
        public static ICollection<TEntry> IfEmpty<TEntry>(
            this ICollection<TEntry> collection,
            Action<ICollection<TEntry>> callback)
        {
            if (collection.IsEmpty())
                callback(collection);

            return collection;
        }
    }
}