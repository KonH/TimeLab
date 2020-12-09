using TimeLab.Command;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	/// <summary>
	/// Takes commands from different sources and writes them into permanent queue,
	/// but only if it isn't simulation, in that case it leads to duplicated reactions
	/// </summary>
	public class CommandRecorder<T> where T : class, ICommand {
		readonly World                    _world;
		readonly PermanentCommandQueue<T> _queue;

		public CommandRecorder(World world, PermanentCommandQueue<T> queue) {
			_world = world;
			_queue = queue;
		}

		public void Record(T command) {
			var timestamp = _world.Time.Current.Value;
			_queue.Enqueue(timestamp, command);
		}
	}
}