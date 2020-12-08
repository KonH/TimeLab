using TimeLab.Shared;
using Zenject;

namespace TimeLab.Systems {
	/// <summary>
	/// Takes commands from queue based on current time and fire signals for each command
	/// </summary>
	public class SignalProducer<T> where T : class {
		readonly TimeProvider      _timeProvider;
		readonly PermanentQueue<T> _queue;
		readonly SignalBus         _bus;

		public SignalProducer(TimeProvider timeProvider, PermanentQueue<T> queue, SignalBus bus) {
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