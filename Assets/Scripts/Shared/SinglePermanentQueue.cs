using System;
using System.Collections.Generic;

namespace TimeLab.Shared {
	/// <summary>
	/// Represents replayable queue, each element has timestamp, associated with it
	/// </summary>
	public sealed class SinglePermanentQueue<T> {
		readonly List<QueueElement<T>> _elements;

		int _lastProducedIndex = -1;

		public int Count => _elements.Count;

		public IReadOnlyList<QueueElement<T>> Elements => _elements;

		public SinglePermanentQueue(List<QueueElement<T>> elements) {
			_elements = elements;
		}

		public SinglePermanentQueue(): this(new List<QueueElement<T>>()) {}

		public void Enqueue(double timestamp, T content) {
			ValidateTimestamp(timestamp);
			_elements.Add(new QueueElement<T>(timestamp, content));
		}

		public void Insert(int position, T content) =>
			_elements.Insert(position, new QueueElement<T>(0, content));

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
	}
}