using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal abstract class Scene : EngineObject
	{
		private readonly Dictionary<int, Actor> Actors;
		public IInputComponent? Input;
		public Scene? Parent;
		public String Title;


		protected Scene(string title)
		{
			Actors = new Dictionary<int, Actor>(1);
			Parent = null;
			Title = title;
		}

		public abstract void Process();

		public virtual void AddActor(Actor actor)
		{
#if INVARIANT
			Invariant.Assert(!Actors.ContainsKey(actor.ID), "Actor is already in the scene.");
#endif
			Actors.Add(actor.ID, actor);
		}

		public virtual void RemoveActor(Actor actor)
		{
#if INVARIANT
			Invariant.Assert(Actors.ContainsKey(actor.ID));
#endif
			Actors.Remove(actor.ID);
		}

		public virtual void RemoveActor(int id)
		{
#if INVARIANT
			Invariant.Assert(Actors.ContainsKey(actor.ID));
#endif
			Actors.Remove(id);
		}

		public virtual void ClearActors()
		{
			Actors.Clear();
		}
	}
}
