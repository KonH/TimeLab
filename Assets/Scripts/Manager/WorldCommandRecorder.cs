using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Manager {
	public sealed class WorldCommandRecorder : CommandRecorder<IWorldCommand> {
		public WorldCommandRecorder(World world, CommandStorage storage) :
			base(world, storage.GetWorldCommands()) {}

		protected override void Enqueue(double timestamp, IWorldCommand command) {
			Debug.Log($"[{timestamp}] {nameof(WorldCommandRecorder)}.{nameof(Enqueue)}: {command}");
			base.Enqueue(timestamp, command);
		}
	}
}