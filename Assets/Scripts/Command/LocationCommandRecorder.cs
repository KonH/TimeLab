using TimeLab.Shared;
using TimeLab.ViewModel;

namespace TimeLab.Command {
	public sealed class LocationCommandRecorder : CommandRecorder<ILocationCommand> {
		public LocationCommandRecorder(TimeProvider timeProvider, Location location, CommandStorage storage) :
			base(timeProvider, storage.GetLocationCommands(location.Id)) {}
	}
}