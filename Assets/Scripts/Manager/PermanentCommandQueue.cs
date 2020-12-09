using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TimeLab.Command;
using TimeLab.Shared;

namespace TimeLab.Manager {
	public sealed class PermanentCommandQueue<T> where T : class, ICommand {
		SinglePermanentQueue<T> _history = new SinglePermanentQueue<T>();
		SinglePermanentQueue<T> _current = new SinglePermanentQueue<T>();

		public void Enqueue(double timestamp, T command) =>
			_current.Enqueue(timestamp, command);

		public bool TryDequeue(double timestamp, out T content) =>
			_history.TryDequeue(timestamp, out content) || _current.TryDequeue(timestamp, out content);

		/// <summary>
		/// Allow to replay commands
		/// </summary>
		public void Reset() {
			_history = Merge(_history, _current);
			_current = new SinglePermanentQueue<T>();
		}

		/// <summary>
		/// Completely forget about all recorded commands
		/// </summary>
		public void Clear() {
			_history = new SinglePermanentQueue<T>();
			_current = new SinglePermanentQueue<T>();
		}

		static SinglePermanentQueue<T> Merge(SinglePermanentQueue<T> history, SinglePermanentQueue<T> current) {
			var currentInputs = current.Elements
				.Where(c => c.Content.GetType().GetCustomAttribute<InputCommandAttribute>() != null)
				.ToArray();
			var allElements = new List<QueueElement<T>>(history.Count + currentInputs.Length);
			allElements.AddRange(history.Elements);
			allElements.AddRange(currentInputs);
			allElements.Sort(Compare);
			return new SinglePermanentQueue<T>(allElements);
		}

		static int Compare(QueueElement<T> left, QueueElement<T> right) =>
			left.Timestamp.CompareTo(right.Timestamp);
	}
}