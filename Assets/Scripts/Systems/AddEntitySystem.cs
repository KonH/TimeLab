using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddEntitySystem : ILocationSystem {
		public AddEntitySystem(Location location, SignalBus bus) {
			bus.Subscribe<AddEntityCommand>(cmd => {
				var entity = new Entity(cmd.Id);
				entity.Position.Value = cmd.Position;
				location.Entities.Add(entity);
				Debug.Log($"{nameof(AddEntitySystem)}: Entity {entity.Id} added to location {location.Id}");
			});
		}
	}
}