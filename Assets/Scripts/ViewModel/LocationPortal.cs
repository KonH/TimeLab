using System.Collections.Generic;

namespace TimeLab.ViewModel {
	public sealed class LocationPortal {
		readonly Dictionary<ulong, Queue<Entity>> _entries = new Dictionary<ulong, Queue<Entity>>();

		public IReadOnlyDictionary<ulong, Queue<Entity>> Entries => _entries;

		public void Enqueue(ulong targetLocationId, Entity entity) {
			if ( !_entries.TryGetValue(targetLocationId, out var queue) ) {
				queue = new Queue<Entity>();
				_entries.Add(targetLocationId, queue);
			}
			queue.Enqueue(entity);
		}
	}
}