using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Manager {
	public sealed class WorldSignalProducer : SignalProducer<IWorldCommand> {
		public WorldSignalProducer(World world, CommandStorage storage, SignalBus bus) :
			base(world, storage.GetWorldCommands(), bus) {}
	}
}