using TimeLab.Command;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	/// <summary>
	/// Operates with current game timeline
	/// </summary>
	public sealed class TimelineController {
		readonly World          _world;
		readonly CommandStorage _commandStorage;
		readonly Session        _session;
		readonly IdGenerator    _idGenerator;

		public TimelineController(
			World world, CommandStorage commandStorage, Session session, IdGenerator idGenerator) {
			_world          = world;
			_commandStorage = commandStorage;
			_session        = session;
			_idGenerator    = idGenerator;
		}

		public void FirstStart() {
			_commandStorage.GetWorldCommands().Enqueue(_world.Time.Current.Value, new AddPlayerCommand(
				_idGenerator.GetNextId(),
				_session.Id));
		}

		public void Restart() {
			_commandStorage.Clear();
		}

		public void RestartWithReplay() {
			_commandStorage.Reset();
		}

		public void TravelBackward() {
			_commandStorage.Reset();
			AddCurrentPlayerForNewWorld();
		}

		void AddCurrentPlayerForNewWorld() {
			_session.Id++;
			var commands = _commandStorage.GetWorldCommands();
			commands.Enqueue(0, new AddPlayerCommand(
				_idGenerator.GetNextId(), _session.Id));
		}
	}
}