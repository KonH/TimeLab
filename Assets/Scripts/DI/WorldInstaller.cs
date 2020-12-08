using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.Systems;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.DI {
	public sealed class WorldInstaller : Installer {
		public override void InstallBindings() {
			SignalBusInstaller.Install(Container);
			Container.Bind<TimeProvider>().AsSingle();
			Container.Bind<TimelineController>().AsSingle();
			Container.Bind<World>().AsSingle();
			Container.Bind<WorldGenerator>().AsSingle();
			Container.Bind<WorldCommandRecorder>().AsSingle();
			Container.Bind<WorldSignalProducer>().AsSingle().NonLazy();
			Container.Bind<LocationContainerHolder>().AsSingle();
			Container.Bind<IdGenerator>().AsSingle();
			Container.Bind<UpdateManager>().AsSingle();
			Container.Bind<PortalManager>().AsSingle();

			Container.DeclareSignal<AddLocationCommand>();
			Container.Bind<AddLocationSystem>().AsSingle().NonLazy();

			Container.DeclareSignal<MovePlayerCommand>();
			Container.Bind<MovePlayerSystem>().AsSingle().NonLazy();

			Container.DeclareSignal<AddPlayerCommand>();
			Container.Bind<AddPlayerSystem>().AsSingle().NonLazy();
		}
	}
}