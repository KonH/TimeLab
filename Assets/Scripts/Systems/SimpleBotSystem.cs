using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Systems {
	public sealed class SimpleBotSystem : ILocationUpdater {
		readonly Timer _timer = new Timer(0.5f);

		readonly TimeProvider            _timeProvider;
		readonly Location                _location;
		readonly LocationCommandRecorder _recorder;

		public SimpleBotSystem(TimeProvider timeProvider, Location location, LocationCommandRecorder recorder) {
			_timeProvider = timeProvider;
			_location     = location;
			_recorder     = recorder;
		}

		public void Update(float deltaTime) {
			if ( !_timeProvider.IsRealTime ) {
				return;
			}
			if ( !_timer.Tick(deltaTime) ) {
				return;
			}
			var bots = _location.Entities
				.Where(t => t.Components.Any(c => c is BotComponent))
				.Select(e => (e, e.Components.OfType<BotComponent>().Single()));
			foreach ( var (entity, component) in bots ) {
				var position = entity.Position.Value;
				if ( position.x >= 5 ) {
					component.MoveLeft = true;
				}
				if ( position.x <= -5 ) {
					component.MoveLeft = false;
				}
				var newPosition = position + (component.MoveLeft ? Vector2Int.left : Vector2Int.right);
				_recorder.TryRecord(null, new MoveEntityCommand(entity.Id, newPosition));
			}
		}
	}
}