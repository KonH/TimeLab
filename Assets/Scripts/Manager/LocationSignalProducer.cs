using TimeLab.Command;
using TimeLab.Shared;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Manager {
	public sealed class LocationSignalProducer : SignalProducer<LocationCommand> {
		public LocationSignalProducer(TimeProvider timeProvider, Location location, CommandStorage storage, SignalBus bus) :
			base(timeProvider, storage.GetLocationCommands(location.Id), bus) {}
	}
}