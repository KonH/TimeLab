using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AIMovementSystem : ILocationSystem {
		public AIMovementSystem(Location location, SignalBus bus, LocationCommandRecorder recorder) {
			bus.Subscribe<ChangeIntentionCommand>(cmd => {
				var sourceId     = cmd.Source;
				var sourceEntity = location.Entities.First(e => e.Id == sourceId);
				var aiComponent  = sourceEntity.Components.OfType<AIComponent>().First();
				var intention    = aiComponent.Intention;
				if ( !(intention is MoveToIntention moveIntention) ) {
					return;
				}
				var targetEntity   = location.Entities.First(e => e.Id == moveIntention.TargetEntity);
				var targetPosition = targetEntity.Position.Value;
				recorder.Record(new MoveEntityCommand(sourceId, targetPosition));
				aiComponent.Intention = null;
			});
		}
	}
}