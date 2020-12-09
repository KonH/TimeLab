using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddEntityComponentSystem {
		public AddEntityComponentSystem(Location location, SignalBus bus) {
			bus.Subscribe<AddEntityComponentCommand>(cmd => {
				var entity = location.Entities.First(e => e.Id == cmd.Entity);
				entity.Components.Add(cmd.Component);
			});
		}
	}
}