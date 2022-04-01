using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fishbowl.Core;
using Fishbowl.Util;

namespace Fishbowl.Core
{
	internal class EngineObject : IComposite
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
			Components = new Dictionary<string, IComponent>(StringReferenceComparer.Instance);
		}

		public EngineObject(string blackboardId)
			: this()
		{
			BlackboardName = string.Intern(blackboardId);
			Simulation.Blackboard.AddItem(blackboardId, this);
		}



		#region Interface: IComposite

		protected virtual Dictionary<string, IComponent> Components { get; set; }

		public virtual IComponent GetComponent(string componentId)
		{
			if (Components.TryGetValue(componentId, out IComponent? retVal))
			{
				return retVal!;
			}

			throw new ComponentNotInCompositeException(componentId, ID);
		}
		
		/// <summary>
		/// Gets a Component as type <typeparamref name="TComponent"/> using an explicit cast.
		/// </summary>
		/// <typeparam name="TComponent">Type of the Component to return.</typeparam>
		/// <param name="componentId">Name of the Component to return.</param>
		/// <returns>Named Component casted to <typeparamref name="TComponent"/></returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="componentId"/> is null/</exception>
		/// <exception cref="ComponentNotInCompositeException">Thrown when the named Component is not present in the Composite.</exception>
		/// <exception cref="InvalidCastException">If the named Component does not match the type <typeparamref name="TComponent"/>.</exception>
		public virtual TComponent GetComponent<TComponent>(string componentId)
			where TComponent : IComponent
		{
			if (Components.TryGetValue(componentId, out IComponent? result))
			{
				return (TComponent)result;
			}

			throw new ComponentNotInCompositeException(componentId, ID);
		}

		public virtual void SetComponent(IComponent component)
		{
			Components[component.ComponentName] = component;
		}

		public virtual void RemoveComponent(string componentId)
		{
#if DEBUG || INVARIANT
			Invariant.Assert(Components.Remove(componentId), $"Component {componentId} not in composite ({ID}).");
#else
			Components.Remove(componentId);
#endif
		}
		#endregion

	}
}
