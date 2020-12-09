using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class RemoveEntitySystem : ILocationSystem {
		public RemoveEntitySystem(Location location, SignalBus bus) {
			bus.Subscribe<RemoveEntityCommand>(cmd => {
				var entity = location.Entities.First(e => e.Id == cmd.Id);
				location.Entities.Remove(entity);
			});
		}
	}
}