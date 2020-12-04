using Zenject;

namespace TimeLab.DI {
	public sealed class SceneInstaller : MonoInstaller {
		public override void InstallBindings() {
			Container.Install<WorldInstaller>();
		}
	}
}