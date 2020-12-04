namespace TimeLab.Shared {
	readonly struct QueueElement<T> {
		public readonly double Timestamp;
		public readonly T      Content;

		public QueueElement(double timestamp, T content) {
			Timestamp = timestamp;
			Content   = content;
		}
	}
}