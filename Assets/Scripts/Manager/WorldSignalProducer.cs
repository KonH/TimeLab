using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Manager {
	public sealed class WorldSignalProducer : SignalProducer<IWorldCommand> {
		public WorldSignalProducer(World world, CommandStorage storage, SignalBus bus) :
			base(world, storage.GetWorldCommands(), bus) {}

		protected override void Fire(IWorldCommand command) {
			Debug.Log($"{nameof(WorldSignalProducer)}.{nameof(Fire)}: {command}");
			base.Fire(command);
		}
	}
}