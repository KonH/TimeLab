using UnityEngine;
using Zenject;

namespace TimeLab.View {
	public sealed class EntityViewInstaller : MonoInstaller {
		[SerializeField] EntityView _prefab;

		public override void InstallBindings() {
			Container.BindMemoryPool<EntityView, EntityView.Pool>()
				.FromComponentInNewPrefab(_prefab);
		}
	}
}