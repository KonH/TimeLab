namespace TimeLab.Shared {
	/// <summary>
	/// Holds actual game start time
	/// </summary>
	public sealed class TimeSettings {
		public double RealStartTime { get; private set; }

		public TimeSettings(double realStartTime) {
			RealStartTime = realStartTime;
		}

		public void Reset(double realStartTime) {
			RealStartTime = realStartTime;
		}
	}
}