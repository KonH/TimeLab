using TimeLab.Command;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	public sealed class WorldCommandRecorder : CommandRecorder<IWorldCommand> {
		public WorldCommandRecorder(World world, CommandStorage storage) :
			base(world, storage.GetWorldCommands()) {}
	}
}