using System.Collections.Generic;
using TimeLab.Shared;

namespace TimeLab.Command {
	/// <summary>
	/// Storage for all game commands to keep state
	/// </summary>
	public sealed class CommandStorage {
		readonly PermanentQueue<IWorldCommand> _worldCommands = new PermanentQueue<IWorldCommand>();

		readonly Dictionary<ulong, PermanentQueue<ILocationCommand>> _locationCommands
			= new Dictionary<ulong, PermanentQueue<ILocationCommand>>();

		public PermanentQueue<IWorldCommand> GetWorldCommands() => _worldCommands;

		public PermanentQueue<ILocationCommand> GetLocationCommands(ulong id) {
			if ( !_locationCommands.TryGetValue(id, out var commands) ) {
				commands = new PermanentQueue<ILocationCommand>();
				_locationCommands.Add(id, commands);
			}
			return commands;
		}

		/// <summary>
		/// Allow to replay commands
		/// </summary>
		public void Reset() {
			_worldCommands.Reset();
			foreach ( var locationCommands in _locationCommands.Values ) {
				locationCommands.Reset();
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