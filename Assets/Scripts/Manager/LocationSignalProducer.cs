using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Manager {
	public sealed class LocationSignalProducer : SignalProducer<ILocationCommand> {
		public LocationSignalProducer(World world, Location location, CommandStorage storage, SignalBus bus) :
			base(world, storage.GetLocationCommands(location.Id), bus) {}
	}
}