using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	public sealed class EntityRenderView : MonoBehaviour {
		public sealed class Pool : MonoMemoryPool<EntityRenderView> {}

		public sealed class PooledFactory {
			[Serializable]
			public sealed class Binding {
				public string Type;
				public EntityRenderView Prefab;
			}

			[Serializable]
			public sealed class Settings {
				public Binding[] Bindings;
			}

			readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

			public PooledFactory(DiContainer container, Settings settings) {
				foreach ( var binding in settings.Bindings ) {
					var pool = container.ResolveId<Pool>(binding.Type);
					_pools.Add(binding.Type, pool);
				}
			}

			public EntityRenderView Spawn(string type) {
				if ( _pools.TryGetValue(type, out var pool) ) {
					return pool.Spawn();
				}
				return null;
			}

			public void Despawn(string type, EntityRenderView item) {
				if ( _pools.TryGetValue(type, out var pool) ) {
					pool.Despawn(item);
				}
			}
		}
	}
}