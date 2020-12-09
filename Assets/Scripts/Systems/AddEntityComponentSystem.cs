using System.Linq;
using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class AddEntityComponentSystem : ILocationSystem {
		public AddEntityComponentSystem(Location location, SignalBus bus) {
			bus.Subscribe<AddEntityComponentCommand>(cmd => {
				var entity = location.Entities.First(e => e.Id == cmd.Entity);
				entity.Components.Add(cmd.Component);
				Debug.Log($"{nameof(AddEntityComponentSystem)}: Component {cmd.Component} added to entity {entity.Id}");
			});
		}
	}
}