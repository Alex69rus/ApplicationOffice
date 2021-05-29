using System.Collections.Generic;

namespace ApplicationOffice.Common.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static void Add<T>(this ICollection<T> collection, IEnumerable<T>? range)
        {
            if (range is null)
                return;

            if (collection is List<T> list)
            {
                list.AddRange(range);
            }
            else
            {
                foreach (var item in range)
                {
                    collection.Add(item);
                }
            }
        }
    }
}