using System.Collections.Generic;
using TimeLab.Shared;
using TimeLab.ViewModel;
using UniRx;
using Zenject;

namespace TimeLab.View {
	public sealed class EntitiesView : DisposableOwner {
		readonly Dictionary<ulong, EntityView> _entities = new Dictionary<ulong, EntityView>();

		EntityView.Pool _pool;

		[Inject]
		public void Init(Location location, EntityView.Pool pool) {
			_pool = pool;
			SetupDisposables();
			Setup(location.Entities);
			location.Entities
				.ObserveAdd()
				.Subscribe(OnAddEntity)
				.AddTo(Disposables);
			location.Entities
				.ObserveRemove()
				.Subscribe(OnRemoveEntity)
				.AddTo(Disposables);
		}

		void Setup(ReactiveCollection<Entity> entities) {
			foreach ( var entity in _entities ) {
				DestroyEntity(entity.Value);
			}
			_entities.Clear();
			foreach ( var entity in entities ) {
				AddEntity(entity);
			}
		}

		void OnAddEntity(CollectionAddEvent<Entity> ev) => AddEntity(ev.Value);

		void OnRemoveEntity(CollectionRemoveEvent<Entity> ev) => RemoveEntity(ev.Value);

		void AddEntity(Entity entity) {
			var instance = _pool.Spawn(transform, entity);
			_entities.Add(entity.Id, instance);
		}

		void RemoveEntity(Entity entity) {
			if ( !_entities.TryGetValue(entity.Id, out var instance) ) {
				return;
			}
			DestroyEntity(instance);
			_entities.Remove(entity.Id);
		}

		void DestroyEntity(EntityView entity) => _pool.Despawn(entity);
	}
}