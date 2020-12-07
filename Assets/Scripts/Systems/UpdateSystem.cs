using TimeLab.Shared;

namespace TimeLab.Systems {
	/// <summary>
	/// Produce all recorded commands for world and each location
	/// </summary>
	public sealed class UpdateSystem {
		readonly TimeProvider            _timeProvider;
		readonly WorldSignalProducer     _signalProducer;
		readonly LocationContainerHolder _holder;
		readonly PortalSystem            _portalSystem;

		public UpdateSystem(
			TimeProvider timeProvider, WorldSignalProducer signalProducer, LocationContainerHolder holder,
			PortalSystem portalSystem) {
			_timeProvider   = timeProvider;
			_signalProducer = signalProducer;
			_holder         = holder;
			_portalSystem   = portalSystem;
		}

		public void Update(float deltaTime = 0) {
			_timeProvider.Advance(deltaTime);
			_portalSystem.Flush();
			_signalProducer.Produce();
			foreach ( var pair in _holder.LocationContainers ) {
				var container = pair.Value;
				var producer  = container.Resolve<LocationSignalProducer>();
				producer.Produce();
			}
		}
	}
}