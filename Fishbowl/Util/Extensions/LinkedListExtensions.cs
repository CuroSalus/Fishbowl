using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	internal static class LinkedListExtensions
	{
		/// <summary>
		/// Finds an index in the <see cref="LinkedList{T}"/>, removes the node, and returns the value.
		/// </summary>
		/// <typeparam name="T">Generic Type parameter of the <see cref="LinkedList"/></typeparam>
		/// <param name="list">List to search.</param>
		/// <param name="index">Place in the list to retrieve and remove.</param>
		/// <returns>Found value at index or <see cref="default(T)"/> otherwise.</returns>
		/// <exception cref="IndexOutOfRangeException">Index is negative or larger than list.</exception>
		/// <exception cref="ArgumentNullException">List is null.</exception>
		public static T? PopIndex<T>(this LinkedList<T> list, int index)
		{
			if (list == null) throw new ArgumentNullException("LinkedList cannot be null.", nameof(list));
			if (index < 0) throw new IndexOutOfRangeException("Index cannot be less than zero.");
			if (index >= list.Count) throw new IndexOutOfRangeException("Index cannot be larger than the size of the linked list.");
			if (list.Count == 0) { return default; }
			
			LinkedListNode<T> node = list.First!;

			for (
				int counter = 0;
				counter < list.Count;
				counter++, node = node.Next
			) {
				if (counter == index)
				{
					T? value = node.Value;
					list.Remove(node);
					return value;
				}

				if (node.Next == null)
					return default;
			}

			return default;
		}
	}
}
