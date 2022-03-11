using Fishbowl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core
{
	internal static partial class Simulation
	{
		public static class Blackboard
		{
			private static readonly Dictionary<string, object?> Map;

			static Blackboard()
			{
				Map = new Dictionary<string, object?>();
			}

			public static void AddItem(string key, object obj) => Map.Add(string.Intern(key), obj);

			public static bool ContainsKey(string key) => Map.ContainsKey(key);

			public static bool ContainsValue(object obj) => Map.ContainsValue(obj);

			public static object? GetItem(string key) => Map[key];

			public static T? GetItem<T>(string key)
			{
				object? item = Map[key];

				if (item == null) return default;
				return (T)item;
			}

			public static bool TryGetItem(string key, out object? obj)
			{
				if (Map.TryGetValue(key, out obj))
					return true;
				obj = null;
				return false;
			}

			public static bool TryGetItem<T>(string key, out T? obj)
			{
				if (Map.TryGetValue(key, out object? value))
				{
					if (value == null)
						obj = default;
					else
						obj = (T)value;

					return true;
				}

				obj = default;
				return false;
			}

			public static void SetItem(string key, object? obj) => Map[key] = obj;
		}
	}
}
