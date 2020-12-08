using TimeLab.Command;
using TimeLab.Shared;

namespace TimeLab.Manager {
	public sealed class WorldCommandRecorder : CommandRecorder<IWorldCommand> {
		public WorldCommandRecorder(TimeProvider timeProvider, CommandStorage storage) :
			base(timeProvider, storage.GetWorldCommands()) {}
	}
}