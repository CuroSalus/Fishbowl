using System.Diagnostics.CodeAnalysis;

namespace Fishbowl.Util
{
	/// <summary>Compares String Equality by referential equality.</summary>
	internal class StringReferenceComparer : IEqualityComparer<string>
	{
		/// <summary>Singleton instance.</summary>
		public static readonly StringReferenceComparer Instance = new();

		/// <summary>Compares two strings by referential equality.</summary>
		public bool Equals(string? x, string? y) => ReferenceEquals(x, y);

		/// <summary>Returns a hash code for the given string.</summary>
		public int GetHashCode([DisallowNull] string obj) => obj.GetHashCode();
	}
}
