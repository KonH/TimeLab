using TimeLab.Command;
using TimeLab.Shared;

namespace TimeLab.Manager {
	/// <summary>
	/// Takes commands from different sources and writes them into permanent queue,
	/// but only if it isn't simulation, in that case it leads to duplicated reactions
	/// </summary>
	public class CommandRecorder<T> where T : class, ICommand {
		readonly TimeProvider             _timeProvider;
		readonly PermanentCommandQueue<T> _queue;

		public CommandRecorder(TimeProvider timeProvider, PermanentCommandQueue<T> queue) {
			_timeProvider = timeProvider;
			_queue        = queue;
		}

		public void Record(T command) {
			var timestamp = _timeProvider.CurrentTime;
			_queue.Enqueue(timestamp, command);
		}
	}
}