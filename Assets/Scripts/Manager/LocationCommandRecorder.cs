using TimeLab.Command;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	public sealed class LocationCommandRecorder : CommandRecorder<ILocationCommand> {
		public LocationCommandRecorder(World world, Location location, CommandStorage storage) :
			base(world, storage.GetLocationCommands(location.Id)) {}
	}
}