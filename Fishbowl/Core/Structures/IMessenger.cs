namespace Fishbowl.Core
{
	internal interface IMessenger : IComponent
	{
		void ReceiveMessage(Message message);
		void SendMessage(Message message);
	}
}
