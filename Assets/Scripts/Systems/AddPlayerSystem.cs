using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddPlayerSystem : IWorldSystem {
		public AddPlayerSystem(IdGenerator idGenerator, World world, SignalBus bus, CommandStorage storage) {
			bus.Subscribe<AddPlayerCommand>(cmd => {
				var id         = idGenerator.GetNextId();
				var locationId = world.Locations.First().Id;
				var commands   = storage.GetLocationCommands(locationId);
				var time       = world.Time.Current.Value;
				commands.Enqueue(time, new AddEntityCommand(id, Vector2Int.zero));
				var components = new IComponent[] {
					new RenderComponent("Player"),
					new PlayerComponent(cmd.Session),
					new CharacterNeed("Hunger", 0),
				};
				foreach ( var component in components ) {
					commands.Enqueue(time, new AddEntityComponentCommand(id, component));
				}
				Debug.Log($"{nameof(AddPlayerSystem)}: player for session {cmd.Session} will be added as {id}");
			});
		}
	}
}