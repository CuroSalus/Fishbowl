using System;
using System.Collections.Generic;

namespace Fishbowl
{
	/// <summary>
	/// Represents the base Composite object for the Component/Composite pattern.<para />
	/// 
	/// IDs are expected to be interned and the Composite should take advantage of this invariant.
	/// </summary>
	internal interface IComposite
	{
		/// <summary>Retrieves a Component from the Composite. Should fail if Component is not found.</summary>
		/// <param name="componentId">Name of the Component to retrieve.</param>
		/// <returns>Composite's corresponding Component with the componentId.</returns>
		IComponent GetComponent(string componentId);

		/// <summary>
		/// Retrieves a Component from the Composite as requested by the the generic parameter <typeparamref name="TComponent"/>.
		/// </summary>
		/// <param name="componentId">Name of the Component to retrieve.</param>
		/// <typeparam name="TComponent">Type of the Component to return.</typeparam>
		/// <returns>Casted Component.</returns>
		TComponent GetComponent<TComponent>(string componentId) where TComponent : IComponent;

		/// <summary>
		/// Sets a Component on a Composite. Should overwrite duplicate Components
		/// unless implementation specifies otherwise.
		/// </summary>
		/// <param name="component">Component to be set on the Composite.</param>
		void SetComponent(IComponent component);

		/// <summary>
		/// Removes a Component from a Composite.
		/// </summary>
		/// <param name="componentId">ID of the component to remove.</param>
		void RemoveComponent(string componentId);
	}
}