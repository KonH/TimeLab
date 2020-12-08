using TimeLab.Shared;

namespace TimeLab.Manager {
	/// <summary>
	/// Operates with current game timeline
	/// </summary>
	public sealed class TimelineController {
		readonly TimeSettings   _timeSettings;
		readonly TimeProvider   _timeProvider;
		readonly CommandStorage _commandStorage;

		public TimelineController(TimeSettings timeSettings, TimeProvider timeProvider, CommandStorage commandStorage) {
			_timeSettings   = timeSettings;
			_timeProvider   = timeProvider;
			_commandStorage = commandStorage;
		}

		public void Restart() {
			_timeSettings.Reset(0);
			_commandStorage.Clear();
		}

		public void RestartWithReplay() {
			_timeSettings.Reset(_timeProvider.CurrentTime);
			_commandStorage.Reset();
		}
	}
}