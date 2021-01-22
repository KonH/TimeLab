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

		readonly World                   _world;
		readonly Location                _location;
		readonly LocationCommandRecorder _recorder;

		public IntentionGeneratorSystem(World world, Location location, LocationCommandRecorder recorder) {
			_world    = world;
			_location = location;
			_recorder = recorder;
		}

		public void Update(float deltaTime) {
			if ( !_timer.Tick(deltaTime) ) {
				return;
			}
			var locationArea       = _location.Components.OfType<RefillArea>().FirstOrDefault();
			var locationRefillType = locationArea?.Type;
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
				if ( maxNeed.Type == locationRefillType ) {
					if ( currentIntention is IdleIntention ) {
						continue;
					}
					Debug.Log($"{nameof(IntentionGeneratorSystem)}: character {entity.Id} intention is to stay in current location to refill '{maxNeed.Type}'");
					ai.Intention = new IdleIntention();
					_recorder.Record(new ChangeIntentionCommand(entity.Id));
					continue;
				}
				if ( currentIntention is MoveToIntention ) {
					continue;
				}
				var refillEntity = _location.Entities.FirstOrDefault(e => e.Components
					.OfType<RefillSource>()
					.Any(s => s.Type == maxNeed.Type));
				if ( refillEntity != null ) {
					Debug.Log(
						$"{nameof(IntentionGeneratorSystem)}: character {entity.Id} should be moved to refill source for '{maxNeed.Type}' using {refillEntity.Id}");
					ai.Intention = new MoveToIntention(refillEntity.Id);
					_recorder.Record(new ChangeIntentionCommand(entity.Id));
					return;
				}
				var suitableLocations = _world.Locations
					.Where(l => l.Components.OfType<RefillArea>().Any(a => a.Type == maxNeed.Type))
					.Select(l => l.Id)
					.ToArray();
				if ( suitableLocations.Length == 0 ) {
					suitableLocations = _world.Locations
						.Where(l => l.Entities.Any(e => e.Components.OfType<RefillSource>().Any(s => s.Type == maxNeed.Type)))
						.Select(l => l.Id)
						.ToArray();
				}
				var door = _location.Entities.FirstOrDefault(e => e.Components
					.OfType<PortalComponent>()
					.Any(p => suitableLocations.Contains(p.TargetLocation)));
				if ( door == null ) {
					continue;
				}
				Debug.Log(
					$"{nameof(IntentionGeneratorSystem)}: character {entity.Id} should be moved to change location to refill '{maxNeed.Type}'");
				ai.Intention = new MoveToIntention(door.Id);
				_recorder.Record(new ChangeIntentionCommand(entity.Id));
			}
		}
	}
}