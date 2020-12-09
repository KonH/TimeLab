using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class MoveEntitySystem : ILocationSystem {
		public MoveEntitySystem(Location location, SignalBus bus) {
			bus.Subscribe<MoveEntityCommand>(cmd => {
				var entity = location.Entities.First(e => e.Id == cmd.Id);
				entity.Position.Value = cmd.Position;
				Debug.Log($"{nameof(MoveEntitySystem)}: entity {entity.Id} moved to {cmd.Position}");
			});
		}
	}
}