using System.Linq;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.ViewModel;

namespace TimeLab.Systems {
	public sealed class CharacterNeedIncreaseSystem : ILocationUpdater {
		readonly Timer _timer = new Timer(1);

		readonly Location                _location;
		readonly LocationCommandRecorder _recorder;

		public CharacterNeedIncreaseSystem(Location location, LocationCommandRecorder recorder) {
			_location = location;
			_recorder = recorder;
		}

		public void Update(float deltaTime) {
			if ( !_timer.Tick(deltaTime) ) {
				return;
			}
			var needs = _location.Entities
				.SelectMany(e => e.Components.OfType<CharacterNeed>());
			foreach ( var need in needs ) {
				need.Amount += 0.1f;
			}
		}
	}
}