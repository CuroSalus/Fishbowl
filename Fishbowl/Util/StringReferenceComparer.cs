using System.Diagnostics.CodeAnalysis;

namespace Fishbowl.Util
{
	internal class StringReferenceComparer : IEqualityComparer<string>
	{
		public static readonly StringReferenceComparer Instance = new();

		public bool Equals(string? x, string? y) => ReferenceEquals(x, y);

		public int GetHashCode([DisallowNull] string obj) => obj.GetHashCode();
	}
}
