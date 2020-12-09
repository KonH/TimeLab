using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddLocationComponentSystem : IWorldSystem {
		public AddLocationComponentSystem(World world, SignalBus bus) {
			bus.Subscribe<AddLocationComponentCommand>(cmd => {
				var location = world.Locations.First(l => l.Id == cmd.Location);
				location.Components.Add(cmd.Component);
			});
		}
	}
}