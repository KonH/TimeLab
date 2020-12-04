using System.Collections.Generic;
using Zenject;

namespace TimeLab.Systems {
	/// <summary>
	/// Holds specific sub-containers for each location
	/// </summary>
	public sealed class LocationContainerHolder {
		public Dictionary<ulong, DiContainer> LocationContainers { get; } = new Dictionary<ulong, DiContainer>();

		public void Add(ulong id, DiContainer subContainer) {
			LocationContainers.Add(id, subContainer);
		}

		public T Resolve<T>(ulong id) =>
			LocationContainers.TryGetValue(id, out var container) ? container.Resolve<T>() : default;
	}
}