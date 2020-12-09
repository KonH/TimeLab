using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class TransferSystem : ILocationSystem {
		public TransferSystem(Location location, SignalBus bus) {
			bus.Subscribe<CollisionCommand>(cmd => {
				var targetEntity = location.Entities.First(e => e.Id == cmd.Target);
				var targetPortal = targetEntity.Components
					.OfType<PortalComponent>()
					.FirstOrDefault();
				if ( targetPortal == null ) {
					return;
				}
				var sourceEntity = location.Entities.First(e => e.Id == cmd.Source);
				var newPosition  = targetPortal.TargetPosition;
				location.Entities.Remove(sourceEntity);
				sourceEntity.Position.Value = newPosition;
				var locationId  = targetPortal.TargetLocation;
				location.Portal.Enqueue(locationId, sourceEntity);
			});
		}
	}
}