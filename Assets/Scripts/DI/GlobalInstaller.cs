using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.DI {
	public sealed class GlobalInstaller : Installer {
		public override void InstallBindings() {
			Container.Bind<Session>().AsSingle();
			Container.BindInstance(new TimeSettings(0));
			Container.Bind<CommandStorage>().AsSingle();
		}
	}
}