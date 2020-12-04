using System.Collections.Generic;
using TimeLab.Shared;
using TimeLab.Systems;
using TimeLab.ViewModel;
using UniRx;
using Zenject;

namespace TimeLab.View {
	/// <summary>
	/// Controls location context management
	/// </summary>
	public sealed class WorldView : DisposableOwner {
		readonly Dictionary<ulong, LocationContext> _locations = new Dictionary<ulong, LocationContext>();

		LocationContext.Pool    _pool;
		LocationContainerHolder _holder;

		[Inject]
		public void Init(World world, LocationContext.Pool pool, LocationContainerHolder holder) {
			_pool   = pool;
			_holder = holder;
			SetupDisposables();
			Setup(world.Locations);
			world.Locations
				.ObserveAdd()
				.Subscribe(OnAddLocation)
				.AddTo(Disposables);
			world.Locations
				.ObserveRemove()
				.Subscribe(OnRemoveLocation)
				.AddTo(Disposables);
		}

		void Setup(ReactiveCollection<Location> locations) {
			foreach ( var loc in _locations ) {
				DestroyLocation(loc.Value);
			}
			_locations.Clear();
			foreach ( var loc in locations ) {
				AddLocation(loc);
			}
		}

		void OnAddLocation(CollectionAddEvent<Location> ev) => AddLocation(ev.Value);

		void OnRemoveLocation(CollectionRemoveEvent<Location> ev) => RemoveLocation(ev.Value);

		void AddLocation(Location location) {
			var instance = _pool.Spawn(transform, location, _holder);
			_locations.Add(location.Id, instance);
		}

		void RemoveLocation(Location location) {
			if ( !_locations.TryGetValue(location.Id, out var instance) ) {
				return;
			}
			DestroyLocation(instance);
			_locations.Remove(location.Id);
		}

		void DestroyLocation(LocationContext location) => _pool.Despawn(location);
	}
}