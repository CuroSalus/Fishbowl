using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal abstract class Scene : EngineObject, IDisplayComponent
	{
		private readonly Dictionary<int, Actor> Actors;
		// public IInputComponent? Input;
		public Scene? Parent;
		public string Title;

		protected Scene(string title)
		{
			Actors = new Dictionary<int, Actor>(1);
			Parent = null;
			Title = title;
			DisplayChildren = new();
		}
		
		protected Scene(string title, IEnumerable<IDisplayComponent> children)
		{
			Actors = new Dictionary<int, Actor>(1);
			Parent = null;
			Title = title;
			DisplayChildren = new List<IDisplayComponent>(children);
		}
		
		public void ProcessInput(ConsoleKeyInfo? key)
		{
			HandleInput(key);
		}

		protected abstract void HandleInput(ConsoleKeyInfo? keyInfo);

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

		#region Interface: IDisplayComponent
		protected readonly List<IDisplayComponent> DisplayChildren;
		
		public abstract bool Enabled { get; set; }
		public abstract int XPosition { get; set; }
		public abstract int YPosition { get; set; }
		public string ComponentName { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

		public void Draw()
		{
			Console.Title = Title;
			Console.Clear();
			for (int i = 0; i < DisplayChildren.Count; i++)
			{
				DisplayChildren[i].Draw();
			}
		}

		public void AddChild(IDisplayComponent child)
		{
			DisplayChildren.Add(child);
		}

		public void RemoveChild(IDisplayComponent child)
		{
			DisplayChildren.Remove(child);
		}
		#endregion
	}
}
