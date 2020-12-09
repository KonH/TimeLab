using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.Systems;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.DI {
	public sealed class LocationInstaller : Installer {
		readonly Location                _location;
		readonly LocationContainerHolder _holder;

		public LocationInstaller(Location location, LocationContainerHolder holder) {
			_location = location;
			_holder   = holder;
		}

		public override void InstallBindings() {
			_holder.Add(_location.Id, Container);

			Container.BindInstance(_location);
			Container.Bind<LocationCommandRecorder>().AsSingle();
			Container.Bind<LocationSignalProducer>().AsSingle().NonLazy();

			Container.Bind<ILocationUpdater>().To<SimpleBotSystem>().AsSingle().NonLazy();

			Container.DeclareSignal<AddEntityCommand>();
			Container.Bind<AddEntitySystem>().AsSingle().NonLazy();

			Container.DeclareSignal<AddComponentCommand>();
			Container.Bind<AddComponentSystem>().AsSingle().NonLazy();

			Container.DeclareSignal<MoveEntityCommand>();
			Container.Bind<MoveEntitySystem>().AsSingle().NonLazy();

			Container.DeclareSignal<CollisionCommand>();
			Container.Bind<CollisionSystem>().AsSingle().NonLazy();
			Container.Bind<TransferSystem>().AsSingle().NonLazy();

		}
	}
}