using TimeLab.Command;
using TimeLab.Shared;
using Zenject;

namespace TimeLab.Systems {
	public sealed class WorldSignalProducer : SignalProducer<IWorldCommand> {
		public WorldSignalProducer(TimeProvider timeProvider, CommandStorage storage, SignalBus bus) :
			base(timeProvider, storage.GetWorldCommands(), bus) {}
	}
}