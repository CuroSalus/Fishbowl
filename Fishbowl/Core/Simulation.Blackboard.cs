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
		public static partial class Blackboard
		{
			public const int MESSAGES_LIMIT = 100;


			private static readonly Dictionary<string, object?> Map;
			private static readonly Message[] _Messages;
			private static byte MessagesPointer = 0;
			public static int MessageCount { get => MessagesPointer; }
			public static readonly ReadOnlyArray<Message> Messages;

			static Blackboard()
			{
				Map = new Dictionary<string, object?>(StringReferenceComparer.Instance);
				_Messages = new Message[MESSAGES_LIMIT];
				Messages = ReadOnlyArray<Message>.WrapArray(_Messages);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="message"></param>
			public static void QueueMessage(Message message)
			{
				if (MessagesPointer == MESSAGES_LIMIT)
				{
					throw new InvalidOperationException("Message queue full!");
				}

				_Messages[MessagesPointer++] = message;
			}

			public static void ClearMessages()
			{
				while (MessagesPointer > 0)
				{
					_Messages[MessagesPointer--] = Message.EmptyMessage();
				}
				_Messages[0] = Message.EmptyMessage();
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
