using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core
{
	internal class EngineObject
	{
		static class ObjectID
		{
			static int CurrentID;

			static ObjectID()
			{
				 CurrentID = 0;
			}

			internal static int GetID() => ++CurrentID;
		}


		public int ID { get; init; }
		public string? BlackboardName { get; init; }


		public EngineObject()
		{
			ID = ObjectID.GetID();
		}

		public EngineObject(string blackboardId)
			: this()
		{
			BlackboardName = string.Intern(blackboardId);
			Simulation.Blackboard.AddItem(blackboardId, this);
		}

	}
}
