using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddComponentSystem {
		public AddComponentSystem(Location location, SignalBus bus) {
			bus.Subscribe<AddComponentCommand>(cmd => {
				var entity = location.Entities.First(e => e.Id == cmd.Entity);
				entity.Components.Add(cmd.Component);
			});
		}
	}
}