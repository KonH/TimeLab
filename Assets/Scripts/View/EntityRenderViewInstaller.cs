using UnityEngine;
using Zenject;

namespace TimeLab.View {
	[CreateAssetMenu]
	public sealed class EntityRenderViewInstaller : ScriptableObjectInstaller {
		[SerializeField] EntityRenderView.PooledFactory.Settings _settings;

		public override void InstallBindings() {
			Container.BindInstance(_settings);
			Container.Bind<EntityRenderView.PooledFactory>().ToSelf().AsSingle();
			foreach ( var binding in _settings.Bindings ) {
				Container.BindMemoryPool<EntityRenderView, EntityRenderView.Pool>()
					.WithId(binding.Type)
					.FromComponentInNewPrefab(binding.Prefab);
			}
		}
	}
}