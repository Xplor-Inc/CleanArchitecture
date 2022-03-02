namespace ExpressCargo.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static T              PickRandom<T>(this IEnumerable<T> source)            => source.PickRandom(1).Single();
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count) => source.Shuffle().Take(count);
        public static IEnumerable<T> Shuffle<T>   (this IEnumerable<T> source)            => source.OrderBy(x => Guid.NewGuid());

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> property)
        {
            return source.GroupBy(property).Select(x => x.First());
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this List<T> source, Func<T, TKey> property)
        {
            return source.GroupBy(property).Select(x => x.First());
        }

        public static IEnumerable<IEnumerable<T>> GroupAdjacentBy<T>(this IEnumerable<T> source, Func<T, T, bool> predicate)
        {
            using var e = source.GetEnumerator();
            if (e.MoveNext())
            {
                var list = new List<T> { e.Current };
                var pred = e.Current;
                while (e.MoveNext())
                {
                    if (predicate(pred, e.Current))
                    {
                        list.Add(e.Current);
                    }
                    else
                    {
                        yield return list;
                        list = new List<T> { e.Current };
                    }
                    pred = e.Current;
                }
                yield return list;
            }
        }
        public static bool IsEmpty<T>(this IEnumerable<T> source)                          => !source.Any();
        public static bool IsEmpty<T>(this IEnumerable<T> source, Func<T, bool> predicate) => !source.Any(predicate);
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)                          => source == null || source.IsEmpty();
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source == null || source.IsEmpty(predicate);
        public static string Join(this IEnumerable<string> list, string delimiter = ", ")
        {
            return list.ToList().Join(delimiter);
        }
        public static string Join(this IEnumerable<KeyValuePair<string, string>> list, string keyValueDelimiter, string delimiter = ", ")
        {
            return list.Select(e => e.Join(keyValueDelimiter)).Join(delimiter);
        }
        public static string Join(this List<string> list, string delimiter = ", ")
        {
            return string.Join(delimiter, list);
        }
        public static string Join(this KeyValuePair<string, string> pair, string delimiter)
        {
            var results = new List<string>
            {
                pair.Key,
                pair.Value
            };

            return results.Where(e => !string.IsNullOrEmpty(e)).Join(delimiter);
        }
    }
}
