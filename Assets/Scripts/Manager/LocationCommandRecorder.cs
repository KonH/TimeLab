using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Manager {
	public sealed class LocationCommandRecorder : CommandRecorder<ILocationCommand> {
		readonly Location _location;

		public LocationCommandRecorder(World world, Location location, CommandStorage storage) :
			base(world, storage.GetLocationCommands(location.Id)) {
			_location = location;
		}

		protected override void Enqueue(double timestamp, ILocationCommand command) {
			Debug.Log($"[{timestamp}] {nameof(LocationCommandRecorder)}.{nameof(Record)} (location: {_location.Id}): {command}");
			base.Enqueue(timestamp, command);
		}
	}
}