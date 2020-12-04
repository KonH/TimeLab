using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddLocationSystem {
		public AddLocationSystem(World world, SignalBus bus) {
			bus.Subscribe<AddLocationCommand>(cmd => {
				var location = new Location(cmd.Id, cmd.Bounds);
				world.Locations.Add(location);
			});
		}
	}
}