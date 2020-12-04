using TimeLab.DI;
using TimeLab.Systems;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	/// <summary>
	/// Manage location injection context for related dependencies
	/// </summary>
	public sealed class LocationContext : MonoBehaviour {
		public sealed class Pool : MonoMemoryPool<Transform, Location, LocationContainerHolder, LocationContext> {
			protected override void Reinitialize(
				Transform parent, Location location, LocationContainerHolder holder, LocationContext item) {
				item.transform.parent = parent;
				item._installer.Init(location, holder);
				item._context.Run();
			}
		}

		[SerializeField] GameObjectContext     _context;
		[SerializeField] MonoLocationInstaller _installer;

	}
}