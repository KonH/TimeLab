using TimeLab.Command;
using TimeLab.Shared;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class LocationSignalProducer : SignalProducer<ILocationCommand> {
		public LocationSignalProducer(TimeProvider timeProvider, Location location, CommandStorage storage, SignalBus bus) :
			base(timeProvider, storage.GetLocationCommands(location.Id), bus) {}
	}
}