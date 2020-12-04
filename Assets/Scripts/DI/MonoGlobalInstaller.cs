using Zenject;

namespace TimeLab.DI {
	public sealed class MonoGlobalInstaller : MonoInstaller {
		public override void InstallBindings() {
			Container.Install<GlobalInstaller>();
		}
	}
}