using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class TransferSystem {
		public TransferSystem(Location location, SignalBus bus) {
			bus.Subscribe<CollisionCommand>(cmd => {
				var targetEntity = cmd.Target;
				var targetPortal = targetEntity.Components
					.OfType<PortalComponent>()
					.FirstOrDefault();
				if ( targetPortal == null ) {
					return;
				}
				var sourceEntity = cmd.Source;
				var newPosition  = targetPortal.TargetPosition;
				location.Entities.Remove(sourceEntity);
				sourceEntity.Position.Value = newPosition;
				var locationId  = targetPortal.TargetLocation;
				location.Portal.Enqueue(locationId, sourceEntity);
			});
		}
	}
}