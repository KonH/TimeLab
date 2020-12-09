using TimeLab.Manager;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.DI {
	public sealed class GlobalInstaller : Installer {
		public override void InstallBindings() {
			Container.Bind<Session>().AsSingle();
			Container.Bind<CommandStorage>().AsSingle();
		}
	}
}