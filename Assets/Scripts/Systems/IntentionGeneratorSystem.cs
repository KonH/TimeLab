using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Systems {
	public sealed class IntentionGeneratorSystem : ILocationUpdater {
		readonly Timer _timer = new Timer(1);

		readonly Location                _location;
		readonly LocationCommandRecorder _recorder;

		public IntentionGeneratorSystem(Location location, LocationCommandRecorder recorder) {
			_location = location;
			_recorder = recorder;
		}

		public void Update(float deltaTime) {
			if ( !_timer.Tick(deltaTime) ) {
				return;
			}
			foreach ( var entity in _location.Entities ) {
				var ai = entity.Components.OfType<AIComponent>().FirstOrDefault();
				if ( ai == null ) {
					continue;
				}
				var currentIntention = ai.Intention;
				var maxNeed = entity.Components
					.OfType<CharacterNeed>()
					.OrderByDescending(n => n.Amount)
					.FirstOrDefault();
				if ( maxNeed == null ) {
					continue;
				}
				var newIntention = maxNeed.Type;
				if ( newIntention == currentIntention ) {
					continue;
				}
				ai.Intention = newIntention;
				Debug.Log($"Character {entity.Id} intention changed to '{newIntention}' (max need: {maxNeed.Amount})");
				_recorder.Record(new ChangeIntentionCommand(entity.Id, newIntention));
			}
		}
	}
}