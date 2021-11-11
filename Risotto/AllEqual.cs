﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risotto.LINQ
{
	static partial class LINQExtensions
	{
		/// <summary>
		/// Checks if all elements in an IEnumerable are equal.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="source">The source sequence.</param>
		/// <returns><c>True</c> if all of the elements in the IEnumerable are equal, <c>False</c> otherwise.</returns>
		/// <exception cref="ArgumentNullException"> if <paramref name="source"/> is null.</exception>
		public static bool AllEqual<TSource>(this IEnumerable<TSource> source)
		{
			return AllEqual(source, EqualityComparer<TSource>.Default);
		}

		/// <summary>
		/// Checks if all elements in an IEnumerable are equal.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="source">The source sequence.</param>
		/// <param name="comparer">The equality comparer to use to determine whether or not values are equal.</param>
		/// <returns><c>True</c> if all of the elements in the IEnumerable are equal, <c>False</c> otherwise.</returns>
		/// <exception cref="ArgumentNullException"> if <paramref name="source"/> is null.</exception>
		public static bool AllEqual<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var first = source.First();

			foreach (var entry in source)
			{
				if (!comparer.Equals(first, entry))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if all elements in an array, based on the provided mapping function.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <typeparam name="TValue">The type of the elements generated by the mapping function.</typeparam>
		/// <param name="source">The source sequence.</param>
		/// <param name="fn">The mapping function.</param>
		/// <returns><c>True</c> if all of the elements in the IEnumerable are equal, <c>False</c> otherwise.</returns>
		/// <exception cref="ArgumentNullException"> if <paramref name="source"/>, or <paramref name="fn"/> is null.</exception>
		public static bool AllEqual<TSource, TValue>(IEnumerable<TSource> source, Func<TSource, TValue> fn)
		{
			return AllEqual(source, fn, EqualityComparer<TValue>.Default);
		}

		/// <summary>
		/// Checks if all elements in an array, based on the provided mapping function.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <typeparam name="TValue">The type of the elements generated by the mapping function.</typeparam>
		/// <param name="source">The source sequence.</param>
		/// <param name="fn">The mapping function.</param>
		/// <param name="comparer">The equality comparer to use to determine whether or not values are equal.</param>
		/// <returns><c>True</c> if all of the elements in the IEnumerable are equal, <c>False</c> otherwise.</returns>
		/// <exception cref="ArgumentNullException"> if <paramref name="source"/>, <paramref name="fn"/>, or <paramref name="comparer"/> is null.</exception>
		public static bool AllEqual<TSource, TValue>(IEnumerable<TSource> source, Func<TSource, TValue> fn, IEqualityComparer<TValue> comparer)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));
			if (fn == null)
				throw new ArgumentNullException(nameof(fn));
			if (comparer == null)
				throw new ArgumentNullException(nameof(comparer));

			List<TValue> processed = new();

			foreach (var value in source)
				processed.Add(fn(value));

			var first = processed[0];
			foreach (var value in processed)
				if (!comparer.Equals(first, value))
					return false;

			return true;
		}
	}
}
