using TimeLab.Systems;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.DI {
	public sealed class MonoLocationInstaller : MonoInstaller {
		Location                _location;
		LocationContainerHolder _holder;

		public void Init(Location location, LocationContainerHolder holder) {
			_location = location;
			_holder   = holder;
		}

		public override void InstallBindings() {
			Container.Install<LocationInstaller>(new object[] { _location, _holder });
		}
	}
}