using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	internal static class ListExtensions
	{
		public static void AddRange<T>(this List<T> list, List<T> items)
		{
			list.Capacity = list.Count + items.Count;

			for (int i = list.Count; i < list.Capacity; i++)
			{
				list.Add(items[i]);
			}
		}

		public static void ForeachDo<T>(this List<T> list, Action<T> action)
		{
			for (int i = 0; i < list.Count; i++)
			{
				action(list[i]);
			}
		}
	}
}
