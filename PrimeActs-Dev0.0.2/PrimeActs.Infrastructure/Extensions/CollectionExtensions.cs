using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                collection.Add(item);
            }
        }

        public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            T element;
            int count = collection.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                element = collection.ElementAt(i);
                if (predicate(element))
                {
                    collection.Remove(element);
                }
            }
        }
    }
}
