using System.Collections.Generic;
using System.Linq;
using TimeLab.Command;

namespace TimeLab.Manager {
	/// <summary>
	/// Storage for all game commands to keep state
	/// </summary>
	public sealed class CommandStorage {
		readonly PermanentCommandQueue<IWorldCommand> _worldCommands = new PermanentCommandQueue<IWorldCommand>();

		readonly Dictionary<ulong, PermanentCommandQueue<ILocationCommand>> _locationCommands
			= new Dictionary<ulong, PermanentCommandQueue<ILocationCommand>>();

		public PermanentCommandQueue<IWorldCommand> GetWorldCommands() => _worldCommands;

		public PermanentCommandQueue<ILocationCommand> GetLocationCommands(ulong id) {
			if ( !_locationCommands.TryGetValue(id, out var commands) ) {
				commands = new PermanentCommandQueue<ILocationCommand>();
				_locationCommands.Add(id, commands);
			}
			return commands;
		}

		/// <summary>
		/// Allow to replay commands
		/// </summary>
		public void Reset(CommandStorage basis) {
			_worldCommands.Reset(basis.GetWorldCommands());
			var allLocations = basis._locationCommands.Keys
				.Concat(_locationCommands.Keys)
				.Distinct();
			foreach ( var location in allLocations ) {
				var locationCommands = GetLocationCommands(location);
				locationCommands.Reset(basis.GetLocationCommands(location));
			}
		}

		/// <summary>
		/// Completely forget about all recorded commands
		/// </summary>
		public void Clear() {
			_worldCommands.Clear();
			_locationCommands.Clear();
		}
	}
}