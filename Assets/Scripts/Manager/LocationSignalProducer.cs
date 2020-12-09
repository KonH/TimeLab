using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Manager {
	public sealed class LocationSignalProducer : SignalProducer<ILocationCommand> {
		readonly Location _location;

		public LocationSignalProducer(World world, Location location, CommandStorage storage, SignalBus bus) :
			base(world, storage.GetLocationCommands(location.Id), bus) {
			_location = location;
		}

		protected override void Fire(ILocationCommand command) {
			Debug.Log($"{nameof(LocationSignalProducer)}.{nameof(Fire)} (location: {_location.Id}): {command}");
			base.Fire(command);
		}
	}
}