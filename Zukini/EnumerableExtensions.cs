using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Zukini
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Shuffles items in Enumerable.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            return source.OrderBy(i => Guid.NewGuid());
        }

        /// <summary>
        /// Retrieves random first item or throws error if item is not present.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorMessage"></param>
        /// <returns>TSource</returns>
        public static TSource RandomFirst<TSource>(this IEnumerable<TSource> source,
            string errorMessage = "Cannot get random item: source is empty")
        {
            return source.Shuffle().FirstOrError(errorMessage);
        }

        /// <summary>
        /// Returns first item or error if item is not present.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorMessage"></param>
        /// <returns>TSource</returns>
        public static TSource FirstOrError<TSource>(this IEnumerable<TSource> source, string errorMessage = "Item is not present.")
        {
            if (source == null) throw new ArgumentException("source");
            var list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0) return list[0];
            }
            else
            {
                using (var e = source.GetEnumerator())
                {
                    if (e.MoveNext()) return e.Current;
                }
            }
            throw new AssertionException(errorMessage);
        }
        
        /// <summary>
        /// Returns first item that matches the predicate or error if item is not present.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="errorMessage"></param>
        /// <returns>TSource</returns>
        public static TSource FirstOrError<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string errorMessage = "Item is not present.")
        {
            if (source == null) throw new ArgumentException("source");
            if (predicate == null) throw new ArgumentException("predicate");
            foreach (var element in source)
            {
                if (predicate(element)) return element;
            }
            throw new AssertionException(errorMessage);
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence
        /// or error if the specified number of elements is not present.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="count">Number of records to return</param>
        /// <param name="errorMessage">Message for AssertionException</param>
        /// <returns>TSource</returns>
        public static IEnumerable<TSource> TakeOrError<TSource>(this IEnumerable<TSource> source, int count, string errorMessage = null)
        {
            var message = errorMessage ?? $"Unable to take {count} number of items.";
            var item = source.Take(count).ToList();
            if (item.Count != count) throw new AssertionException(message);
            return item;
        }

        /// <summary>
        /// Groups source by distinct field.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var known = new HashSet<TKey>();
            return source.Where(element => known.Add(keySelector(element)));
        }
    }
}
