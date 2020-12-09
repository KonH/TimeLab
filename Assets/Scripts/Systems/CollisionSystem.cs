using System.Linq;
using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class CollisionSystem : ILocationSystem {
		public CollisionSystem(Location location, SignalBus bus, LocationCommandRecorder recorder) {
			bus.Subscribe<MoveEntityCommand>(cmd => {
				var sourceId       = cmd.Id;
				var sourceEntity   = location.Entities.First(e => e.Id == sourceId);
				var targetPosition = cmd.Position;
				var targetEntities = location.Entities
					.Where(e => (e.Id != sourceId) && (e.Position.Value == targetPosition));
				foreach ( var targetEntity in targetEntities ) {
					recorder.Record(new CollisionCommand(sourceEntity.Id, targetEntity.Id));
					Debug.Log($"{nameof(CollisionSystem)}: collision between {sourceEntity.Id} and {targetEntity.Id} detected");
				}
			});
		}
	}
}