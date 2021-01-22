using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AIMovementSystem : ILocationSystem {
		public AIMovementSystem(World world, Location location, SignalBus bus, LocationCommandRecorder recorder) {
			bus.Subscribe<ChangeIntentionCommand>(cmd => {
				var target    = cmd.Target;
				var intention = cmd.NewIntention;
				var refillEntity = location.Entities.FirstOrDefault(e => e.Components
					.OfType<RefillSource>()
					.Any(s => s.Type == intention));
				if ( refillEntity != null ) {
					var position = refillEntity.Position.Value;
					Debug.Log(
						$"{nameof(AIMovementSystem)}: entity {target} should be moved to refill source for '{intention}' at {position}");
					recorder.Record(new MoveEntityCommand(target, position));
					return;
				}
				var suitableLocations = world.Locations
					.Where(l => l.Components.OfType<RefillArea>().Any(a => a.Type == intention))
					.Select(l => l.Id)
					.ToArray();
				var door = location.Entities.FirstOrDefault(e => e.Components
					.OfType<PortalComponent>()
					.Any(p => suitableLocations.Contains(p.TargetLocation)));
				if ( door != null ) {
					var position = door.Position.Value;
					Debug.Log(
						$"{nameof(AIMovementSystem)}: entity {target} should be moved to refill area for '{intention}' using door at {position}");
					recorder.Record(new MoveEntityCommand(target, position));
				}
			});
		}
	}
}