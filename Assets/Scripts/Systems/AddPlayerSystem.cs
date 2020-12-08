using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddPlayerSystem {
		public AddPlayerSystem(TimeProvider timeProvider, SignalBus bus, World world, CommandStorage storage) {
			bus.Subscribe<AddPlayerCommand>(cmd => {
				var locationId = world.Locations.First().Id;
				var components = new IComponent[] {
					new RenderComponent("Player"),
					new PlayerComponent(cmd.Session)
				};
				storage.GetLocationCommands(locationId).Enqueue(
					timeProvider.CurrentTime, new AddEntityCommand(cmd.Id, Vector2Int.zero, components));
			});
		}
	}
}