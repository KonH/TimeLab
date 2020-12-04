using TimeLab.Command;
using TimeLab.Shared;
using Zenject;

namespace TimeLab.DI {
	public sealed class GlobalInstaller : Installer {
		public override void InstallBindings() {
			Container.BindInstance(new TimeSettings(0));
			Container.Bind<CommandStorage>().AsSingle();
		}
	}
}