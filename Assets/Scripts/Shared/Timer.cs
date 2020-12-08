namespace TimeLab.Shared {
	public sealed class Timer {
		readonly float _interval;

		float _timer;

		public Timer(float interval) {
			_interval = interval;
		}

		public bool Tick(float deltaTime) {
			_timer += deltaTime;
			if ( _timer < _interval ) {
				return false;
			}
			_timer -= _interval;
			return true;
		}
	}
}