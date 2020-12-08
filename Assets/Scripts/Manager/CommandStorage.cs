using System.Collections.Generic;
using TimeLab.Command;

namespace TimeLab.Manager {
	/// <summary>
	/// Storage for all game commands to keep state
	/// </summary>
	public sealed class CommandStorage {
		readonly PermanentCommandQueue<WorldCommand> _worldCommands = new PermanentCommandQueue<WorldCommand>();

		readonly Dictionary<ulong, PermanentCommandQueue<LocationCommand>> _locationCommands
			= new Dictionary<ulong, PermanentCommandQueue<LocationCommand>>();

		public PermanentCommandQueue<WorldCommand> GetWorldCommands() => _worldCommands;

		public PermanentCommandQueue<LocationCommand> GetLocationCommands(ulong id) {
			if ( !_locationCommands.TryGetValue(id, out var commands) ) {
				commands = new PermanentCommandQueue<LocationCommand>();
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