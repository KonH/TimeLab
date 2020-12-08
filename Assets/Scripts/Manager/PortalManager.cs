using System.Linq;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	public sealed class PortalManager {
		readonly World _world;

		public PortalManager(World world) {
			_world = world;
		}

		public void Flush() {
			foreach ( var location in _world.Locations ) {
				foreach ( var portal in location.Portal.Entries ) {
					var entities = portal.Value;
					if ( entities.Count == 0 ) {
						continue;
					}
					var destination = _world.Locations.First(l => l.Id == portal.Key);
					while ( entities.Count > 0 ) {
						var entity = entities.Dequeue();
						destination.Entities.Add(entity);
					}
				}
			}
		}
	}
}