using TimeLab.Shared;

namespace TimeLab.Manager {
	/// <summary>
	/// Takes commands from different sources and writes them into permanent queue,
	/// but only if it isn't simulation, in that case it leads to duplicated reactions
	/// </summary>
	public class CommandRecorder<T> {
		readonly TimeProvider      _timeProvider;
		readonly PermanentQueue<T> _queue;

		public CommandRecorder(TimeProvider timeProvider, PermanentQueue<T> queue) {
			_timeProvider = timeProvider;
			_queue        = queue;
		}

		public bool TryRecord(T command) {
			if ( !_timeProvider.IsRealTime ) {
				return false;
			}
			var timestamp = _timeProvider.CurrentTime;
			_queue.Enqueue(timestamp, command);
			return true;
		}
	}
}