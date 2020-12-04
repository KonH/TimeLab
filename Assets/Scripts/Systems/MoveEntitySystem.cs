using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class MoveEntitySystem {
		public MoveEntitySystem(Location location, SignalBus bus) {
			bus.Subscribe<MoveEntityCommand>(cmd => {
				var entity = location.Entities.First(e => e.Id == cmd.Id);
				entity.Position.Value = cmd.Position;
			});
		}
	}
}