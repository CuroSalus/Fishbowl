using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	internal static class DictionaryExtensions
	{
		/// <summary>
		/// Add a collection of key/value pairs to a dictionary.
		/// </summary>
		/// <typeparam name="TKey">Key type</typeparam>
		/// <typeparam name="TObject">Object type</typeparam>
		/// <param name="dictionary">Dictionary to add to.</param>
		/// <param name="collection">Enumerable collection of objects to add.</param>
		/// <param name="keySelector">Returns the key value from the object.</param>
		public static void AddRange<TKey, TObject>(this IDictionary<TKey, TObject> dictionary, IEnumerable<TObject> collection, Func<TObject, TKey> keySelector)
		{
			foreach (TObject item in collection)
			{
				dictionary.Add(keySelector(item), item);
			}
		}
	}
}
