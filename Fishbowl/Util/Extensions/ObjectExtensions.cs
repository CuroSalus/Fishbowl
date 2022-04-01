using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	public static class ObjectExtensions
	{
		public static T As<T>(this object obj)
		{
			return (T)obj;
		}

		public static bool Exists([NotNullWhen(true)] this object obj)
		{
			return obj != null;
		}
	}
}
