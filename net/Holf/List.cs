namespace Holf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class List
    {
        /// <summary>
        /// Map each item in list to another list
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="fn">The mapping function</param>
        /// <param name="list">The processed list</param>
        /// <returns>A list that where each item has been mapped</returns>
        public static IEnumerable<U> Map<T, U>(Func<T, U> fn, IEnumerable<T> list)
        {
            if (fn == null)
            {
                throw new ArgumentNullException("fn", "Supplied function to Map is not allowed to be null");
            }

            if (list == null)
            {
                throw new ArgumentNullException("list", "Supplied list to Map is not allowed to be null");
            }

            foreach (var item in list)
            {
                yield return fn(item);
            }
        }

        /// <summary>
        /// Map each item in list to another list
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="list">The processed list</param>
        /// <param name="fn">The mapping function</param>
        /// <returns>A list that where each item has been mapped</returns>
        public static IEnumerable<U> Map<T, U>(this IEnumerable<T> list, Func<T, U> fn)
        {
            return Map(fn, list);
        }

        /// <summary>
        /// Aggregates values in list with init as seed value.
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="fn">The aggregate function</param>
        /// <param name="list">The processed list</param>
        /// <param name="init">The initial seed value for the aggregation</param>
        /// <returns>An aggregated value of all items in the list, aggregated with the fn function</returns>
        public static U Fold<T, U>(Func<U, T, U> fn, IEnumerable<T> list, U init)
        {
            if (fn == null)
            {
                throw new ArgumentNullException("fn", "Supplied function to Fold is not allowed to be null");
            }
            
            if (list == null)
            {
                throw new ArgumentNullException("list", "Supplied list to Fold is not allowed to be null");
            }

            var result = init;
            foreach (var item in list)
            {
                result = fn(result, item);
            }

            return result;
        }

        /// <summary>
        /// Aggregates values in list with init as seed value.
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="list">The processed list</param>
        /// <param name="fn">The aggregate function</param>
        /// <param name="init">The initial seed value for the aggregation</param>
        /// <returns>An aggregated value of all items in the list, aggregated with the fn function</returns>
        public static U Fold<T, U>(this IEnumerable<T> list, Func<U, T, U> fn, U init)
        {
            return Fold(fn, list, init);
        }

        /// <summary>
        /// Partitions the items in the list according to function
        /// </summary>
        /// <typeparam name="T">Type of the list items</typeparam>
        /// <param name="fn">Function returning a positive int value specifying what partition the item should be put in.</param>
        /// <param name="list">The list that should be partitioned</param>
        /// <returns>A partitioned list, an array of enumerables</returns>
        public static IEnumerable<T>[] Partition<T>(Func<T, int> fn, IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list", "Supplied list to Partition is not allowed to be null");
            }

            if (fn == null)
            {
                throw new ArgumentNullException("fn", "Supplied function to Partition is not allowed to be null");
            }

            // Expand the array arr to fit index i
            Func<IList<T>[], int, IList<T>[]> expand = (arr, i) =>
            {
                var newArray = new IList<T>[i + 1];
                Array.Copy(arr, newArray, arr.Length);

                // Initialize new array
                for (var k = arr.Length; k < newArray.Length; k++)
                {
                    newArray[k] = new List<T>();
                }

                return newArray;
            };

            var result = new IList<T>[0];
            foreach (var item in list)
            {
                var index = fn(item);
                if (index < 0)
                {
                    throw new IndexOutOfRangeException("Your partition function returned index less than 0");
                }

                // new index
                if (index > result.Length - 1)
                {
                    // expand array
                    result = expand(result, index);
                }

                result[index].Add(item);
            }

            return result;
        }

        /// <summary>
        /// Partitions the items in the list according to function
        /// </summary>
        /// <typeparam name="T">Type of the list items</typeparam>
        /// <param name="list">The list that should be partitioned</param>
        /// <param name="fn">Function returning a positive int value specifying what partition the item should be put in.</param>
        /// <returns>A partitioned list, an array of enumerables</returns>
        public static IEnumerable<T>[] Partition<T>(this IEnumerable<T> list, Func<T, int> fn)
        {
            return Partition(fn, list);
        }

        /// <summary>
        /// Reduces list to a specific value.
        /// </summary>
        /// <typeparam name="T">Type of the list items.</typeparam>
        /// <param name="fn">The aggregate function that determines how to aggregate two values.</param>
        /// <param name="list">The list that should be reduced.</param>
        /// <returns>A single value that the list has been reduced to.</returns>
        public static T Reduce<T>(Func<T, T, T> fn, IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list", "Supplied list to Reduce is not allowed to be null");
            }

            if (fn == null)
            {
                throw new ArgumentNullException("fn", "Supplied function to Reduce is not allowed to be null");
            }

            IEnumerator<T> enumerator = list.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Can't run reduce on an empty list", "list");
            }

            var result = enumerator.Current;
            while (enumerator.MoveNext())
            {
                result = fn(result, enumerator.Current);
            }

            return result;
        }

        /// <summary>
        /// Reduces list to a specific value.
        /// </summary>
        /// <typeparam name="T">Type of the list items.</typeparam>
        /// <param name="list">The list that should be reduced.</param>
        /// <param name="fn">The aggregate function that determines how to aggregate two values.</param>
        /// <returns>A single value that the list has been reduced to.</returns>
        public static T Reduce<T>(this IEnumerable<T> list, Func<T, T, T> fn)
        {
            return Reduce(fn, list);
        }

        /// <summary>
        /// Expands initial value into a list
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="fn">Function for calculating next value</param>
        /// <param name="init">The initial value</param>
        /// <returns>A list with values</returns>
        public static IEnumerable<T> Expand<T>(Func<T, T> fn, T init)
        {
            if (fn == null)
            {
                throw new ArgumentNullException("fn", "Supplied fn argument to Expand is not allowed to be null");
            }

            var current = init;
            yield return current; // yield initial value
            while (true)
            {
                // yield each calculated new value
                yield return current = fn(current);
            }
        }

        /// <summary>
        /// Expands initial value into a list
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="init">The initial value</param>
        /// <param name="fn">Function for calculating next value</param>
        /// <returns>A list with values</returns>
        public static IEnumerable<T> Expand<T>(this T init, Func<T, T> fn)
        {
            return Expand(fn, init);
        }

        /// <summary>
        /// Scans returns calculation for each item in the list
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="fn">The calculation function</param>
        /// <param name="state">The state brought between functions</param>
        /// <param name="list">The list to be processed</param>
        /// <returns>A list of finished calculations</returns>
        public static IEnumerable<U> Scan<T, U>(Func<U, T, U> fn, U state, IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list", "Supplied list to Scan is not allowed to be null");
            }

            if (fn == null)
            {
                throw new ArgumentNullException("fn", "Supplied function to Scan is not allowed to be null");
            }

            var result = state;
            foreach (var item in list)
            {
                yield return result = fn(result, item);
            }
        }

        /// <summary>
        /// Scans returns calculation for each item in the list
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="list">The list to be processed</param>
        /// <param name="fn">The calculation function</param>
        /// <param name="state">The state brought between functions</param>
        /// <returns>A list of finished calculations</returns>
        public static IEnumerable<U> Scan<T, U>(this IEnumerable<T> list, Func<U, T, U> fn, U state)
        {
            return Scan(fn, state, list);
        }

        /// <summary>
        /// Merge collections that are calculated through the function
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="fn">The function that yields a collection</param>
        /// <param name="list">The list list to be processed</param>
        /// <returns>A flat collection</returns>
        public static IEnumerable<U> Collect<T, U>(Func<T, IEnumerable<U>> fn, IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list", "Supplied list to Collect is not allowed to be null");
            }

            if (fn == null)
            {
                throw new ArgumentNullException("fn", "Supplied function to Collect is not allowed to be null");
            }

            foreach (var item1 in list)
            { 
                foreach (var item2 in fn(item1))
                {
                    yield return item2;
                }
            }
        }

        /// <summary>
        /// Merge collections that are calculated through the function
        /// </summary>
        /// <typeparam name="T">Incoming type</typeparam>
        /// <typeparam name="U">Outgoing type</typeparam>
        /// <param name="list">The original list</param>
        /// <param name="fn">The function that yields a collection</param>
        /// <returns>A flat collection</returns>
        public static IEnumerable<U> Collect<T, U>(this IEnumerable<T> list, Func<T, IEnumerable<U>> fn)
        {
            return Collect(fn, list);
        }
    }
}