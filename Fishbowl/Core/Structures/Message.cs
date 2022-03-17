using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Fishbowl.Core
{
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	internal struct Message
	{
		[FieldOffset(0)]
		public readonly int SenderID;
		[FieldOffset(4)]
		public readonly int ReceiverID;
		[FieldOffset(8)]
		public readonly int Value;

		public Message(int senderID, int receiverID, int value)
		{
			SenderID = senderID;
			ReceiverID = receiverID;
			Value = value;
		}

		public static Message EmptyMessage() => new(0, 0, 0);

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (GetType() != obj?.GetType()) { return false; }

			Message other = (Message)obj;

			return SenderID == other.SenderID && ReceiverID == other.ReceiverID && Value == other.Value;
		}

		public override int GetHashCode() => (SenderID << 16) ^ ReceiverID ^ Value;
	}
}
