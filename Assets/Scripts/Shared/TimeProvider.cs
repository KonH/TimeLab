namespace TimeLab.Shared {
	/// <summary>
	/// Holds time information: what is game time now and when a player is actually starts it
	/// </summary>
	public sealed class TimeProvider {
		public double RealStartTime { get; }
		public double CurrentTime   { get; private set; }

		public bool IsRealTime => CurrentTime >= RealStartTime;

		public TimeProvider(TimeSettings settings) {
			RealStartTime = settings.RealStartTime;
		}

		public void Advance(float dt) {
			CurrentTime += dt;
		}
	}
}