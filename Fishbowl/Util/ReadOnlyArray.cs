using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Util
{
	internal sealed class ReadOnlyArray<T> : ICloneable, IList, IStructuralComparable, IStructuralEquatable
	{
		public static ReadOnlyArray<TType> WrapArray<TType>(TType[] target)
			=> new() { InternalArray = target };

		private T[] InternalArray { get; init; }

		#region Constructors
		private ReadOnlyArray()
		{
			InternalArray = Array.Empty<T>();
		}

		public ReadOnlyArray(T[] array)
		{
			InternalArray = new T[array.Length];
			Array.Copy(array, 0, InternalArray, 0, array.Length);
		}

		public ReadOnlyArray(ReadOnlyArray<T> array)
			: this(array.InternalArray)
		{ }
		#endregion

		public T[] ToArray()
		{
			T[] retArr = new T[InternalArray.Length];
			Array.Copy(InternalArray, 0, retArr, 0, InternalArray.Length);
			return retArr;
		}
		public int Length => InternalArray.Length;


		#region Operators
		public static implicit operator ReadOnlyArray<T>(T[] arr) => new(arr);

		public T this[int i] { get => InternalArray[i]; }
		#endregion



		#region Interfaces
		#region Interface: ICloneable
		public object Clone() => new ReadOnlyArray<T>(InternalArray);
		#endregion



		#region Interface: IList
		bool IList.IsFixedSize => true;
		bool IList.IsReadOnly => true;
		object? IList.this[int index] { get => this[index]; set => throw new InvalidOperationException("Array is readonly."); }
		int IList.Add(object? value) => throw new InvalidOperationException("Array is readonly.");

		void IList.Clear() => throw new InvalidOperationException("Array is readonly.");

		bool IList.Contains(object? value) => Array.IndexOf(InternalArray, value) >= 0;

		int IList.IndexOf(object? value) => Array.IndexOf(InternalArray, value);

		void IList.Insert(int index, object? value) => throw new InvalidOperationException("Array is readonly.");

		void IList.Remove(object? value) => throw new InvalidOperationException("Array is readonly.");

		void IList.RemoveAt(int index) => throw new InvalidOperationException("Array is readonly.");
		#endregion



		#region Interface: IColleciton
		int ICollection.Count => InternalArray.Length;

		bool ICollection.IsSynchronized => InternalArray.IsSynchronized;

		object ICollection.SyncRoot => InternalArray.SyncRoot;

		void ICollection.CopyTo(Array array, int index) => Array.Copy(InternalArray, array, InternalArray.Length);
		#endregion



		#region Interface: IEnumerable
		IEnumerator IEnumerable.GetEnumerator() => InternalArray.GetEnumerator();
		#endregion



		#region Interface: IStructuralComparable
		public int CompareTo(object? other, IComparer comparer) => ((IStructuralComparable)InternalArray).CompareTo(other, comparer);
		#endregion



		#region Interface: IStructuralEquatable
		public bool Equals(object? other, IEqualityComparer comparer) => ((IStructuralEquatable)InternalArray).Equals(other, comparer);

		public int GetHashCode(IEqualityComparer comparer) => ((IStructuralEquatable)InternalArray).GetHashCode(comparer);
		#endregion
		#endregion
	}
}
