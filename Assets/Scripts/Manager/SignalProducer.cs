using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Manager {
	/// <summary>
	/// Takes commands from queue based on current time and fire signals for each command
	/// </summary>
	public class SignalProducer<T> where T : class, ICommand {
		readonly World                    _world;
		readonly PermanentCommandQueue<T> _queue;
		readonly SignalBus                _bus;

		public SignalProducer(World world, PermanentCommandQueue<T> queue, SignalBus bus) {
			_world = world;
			_queue = queue;
			_bus   = bus;
		}

		public void Produce() {
			var timestamp = _world.Time.Current.Value;
			while ( _queue.TryDequeue(timestamp, out var command) ) {
				Fire(command);
			}
		}

		protected virtual void Fire(T command) {
			_bus.TryFire((object)command);
		}
	}
}