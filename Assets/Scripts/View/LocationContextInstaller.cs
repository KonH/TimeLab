using UnityEngine;
using Zenject;

namespace TimeLab.View {
	public sealed class LocationContextInstaller : MonoInstaller {
		[SerializeField] LocationContext _prefab;

		public override void InstallBindings() {
			Container.BindMemoryPool<LocationContext, LocationContext.Pool>()
				.FromComponentInNewPrefab(_prefab);
		}
	}
}