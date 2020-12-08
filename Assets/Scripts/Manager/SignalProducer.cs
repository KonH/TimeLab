using TimeLab.Command;
using TimeLab.Shared;
using Zenject;

namespace TimeLab.Manager {
	/// <summary>
	/// Takes commands from queue based on current time and fire signals for each command
	/// </summary>
	public class SignalProducer<T> where T : class, ICommand {
		readonly TimeProvider             _timeProvider;
		readonly PermanentCommandQueue<T> _queue;
		readonly SignalBus                _bus;

		public SignalProducer(TimeProvider timeProvider, PermanentCommandQueue<T> queue, SignalBus bus) {
			_timeProvider = timeProvider;
			_queue        = queue;
			_bus          = bus;
		}

		public void Produce() {
			var timestamp = _timeProvider.CurrentTime;
			while ( _queue.TryDequeue(timestamp, out var command) ) {
				_bus.TryFire((object)command);
			}
		}
	}
}