using TimeLab.Shared;
using TimeLab.Systems;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	/// <summary>
	/// Produce all recorded commands for world and each location
	/// </summary>
	public sealed class WorldUpdater : MonoBehaviour {
		TimeProvider            _timeProvider;
		WorldSignalProducer     _signalProducer;
		LocationContainerHolder _holder;

		[Inject]
		public void Init(TimeProvider timeProvider, WorldSignalProducer signalProducer, LocationContainerHolder holder) {
			_timeProvider   = timeProvider;
			_signalProducer = signalProducer;
			_holder         = holder;
		}

		void Update() {
			_timeProvider.Advance(Time.deltaTime);
			_signalProducer.Produce();
			foreach ( var pair in _holder.LocationContainers ) {
				var container = pair.Value;
				var producer = container.Resolve<LocationSignalProducer>();
				producer.Produce();
			}
		}
	}
}