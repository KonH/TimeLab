using TimeLab.ViewModel;

namespace TimeLab.Manager {
	/// <summary>
	/// Produce all recorded commands for world and each location
	/// </summary>
	public sealed class UpdateManager {
		readonly World                   _world;
		readonly WorldSignalProducer     _signalProducer;
		readonly LocationContainerHolder _holder;
		readonly PortalManager           _portalManager;

		public UpdateManager(
			World world, WorldSignalProducer signalProducer, LocationContainerHolder holder,
			PortalManager portalManager) {
			_world          = world;
			_signalProducer = signalProducer;
			_holder         = holder;
			_portalManager  = portalManager;
		}

		public void Update(float deltaTime = 0) {
			_portalManager.Flush();
			_signalProducer.Produce();
			foreach ( var pair in _holder.LocationContainers ) {
				var container = pair.Value;
				var producer  = container.Resolve<LocationSignalProducer>();
				producer.Produce();
				var updateSystems = container.ResolveAll<ILocationUpdater>();
				foreach ( var updateSystem in updateSystems ) {
					updateSystem.Update(deltaTime);
				}
			}
			_world.Time.Current.Value += deltaTime;
		}
	}
}