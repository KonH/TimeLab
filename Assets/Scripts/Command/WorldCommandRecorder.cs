using TimeLab.Shared;

namespace TimeLab.Command {
	public sealed class WorldCommandRecorder : CommandRecorder<IWorldCommand> {
		public WorldCommandRecorder(TimeProvider timeProvider, CommandStorage storage) :
			base(timeProvider, storage.GetWorldCommands()) {}
	}
}