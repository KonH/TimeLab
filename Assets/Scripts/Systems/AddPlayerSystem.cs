using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddPlayerSystem {
		public AddPlayerSystem(World world, SignalBus bus, CommandStorage storage) {
			bus.Subscribe<AddPlayerCommand>(cmd => {
				var locationId = world.Locations.First().Id;
				var components = new IComponent[] {
					new RenderComponent("Player"),
					new PlayerComponent(cmd.Session)
				};
				storage.GetLocationCommands(locationId).Enqueue(
					world.Time.Current.Value, new AddEntityCommand(cmd.Id, Vector2Int.zero, components));
			});
		}
	}
}