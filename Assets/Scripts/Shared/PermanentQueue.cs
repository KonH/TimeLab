using System;
using System.Collections.Generic;

namespace TimeLab.Shared {
	/// <summary>
	/// Represents replayable queue, each element has timestamp, associated with it
	/// </summary>
	public sealed class PermanentQueue<T> {
		readonly List<QueueElement<T>> _elements = new List<QueueElement<T>>();

		int _lastProducedIndex = -1;

		public int Count => _elements.Count;

		public void Enqueue(double timestamp, T content) {
			ValidateTimestamp(timestamp);
			_elements.Add(new QueueElement<T>(timestamp, content));
		}

		public void Insert(int index, T content) {
			_elements.Insert(index, new QueueElement<T>(0, content));
		}

		void ValidateTimestamp(double timestamp) {
			if ( _elements.Count == 0 ) {
				return;
			}
			var lastTimestamp = _elements[_elements.Count - 1].Timestamp;
			if ( lastTimestamp > timestamp ) {
				throw new InvalidOperationException(
					$"New content timestamp ({timestamp}) is less then last timestamp ({lastTimestamp})");
			}
		}

		public bool TryDequeue(double timestamp, out T content) {
			for ( var i = (_lastProducedIndex + 1); i < _elements.Count; i++ ) {
				var element = _elements[i];
				if ( timestamp >= element.Timestamp ) {
					_lastProducedIndex = i;
					content    = element.Content;
					return true;
				}
			}
			content = default;
			return false;
		}

		/// <summary>
		/// Allow to replay commands
		/// </summary>
		public void Reset() {
			_lastProducedIndex = -1;
		}

		/// <summary>
		/// Completely forget about all recorded commands
		/// </summary>
		public void Clear() {
			Reset();
			_elements.Clear();
		}
	}
}