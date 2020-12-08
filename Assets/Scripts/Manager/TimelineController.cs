using TimeLab.Command;
using TimeLab.Shared;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	/// <summary>
	/// Operates with current game timeline
	/// </summary>
	public sealed class TimelineController {
		readonly TimeSettings   _timeSettings;
		readonly TimeProvider   _timeProvider;
		readonly CommandStorage _commandStorage;
		readonly Session        _session;
		readonly IdGenerator    _idGenerator;

		public TimelineController(
			TimeSettings timeSettings, TimeProvider timeProvider, CommandStorage commandStorage,
			Session session, IdGenerator idGenerator) {
			_timeSettings   = timeSettings;
			_timeProvider   = timeProvider;
			_commandStorage = commandStorage;
			_session        = session;
			_idGenerator    = idGenerator;
		}

		public void FirstStart() {
			_commandStorage.GetWorldCommands().Enqueue(_timeProvider.CurrentTime, new AddPlayerCommand(
				_idGenerator.GetNextId(),
				_session.Id));
		}

		public void Restart() {
			_timeSettings.Reset(0);
			_commandStorage.Clear();
		}

		public void RestartWithReplay() {
			_timeSettings.Reset(_timeProvider.CurrentTime);
			_commandStorage.Reset();
		}

		public void TravelBackward() {
			_timeSettings.Reset(_timeProvider.CurrentTime);
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