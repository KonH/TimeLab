using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class RemovePlayerSystem : IWorldSystem {
		public RemovePlayerSystem(World world, SignalBus bus, CommandStorage storage) {
			bus.Subscribe<RemovePlayerCommand>(cmd => {
				foreach ( var location in world.Locations ) {
					var players = location.Entities
						.Where(e => e.Components
							.Any(c => (c is PlayerComponent player) && player.Session == cmd.Session));
					foreach ( var player in players ) {
						var time = world.Time.Current.Value;
						storage.GetLocationCommands(location.Id).Enqueue(time, new RemoveEntityCommand(player.Id));
						Debug.Log($"{nameof(RemovePlayerCommand)}: player {player.Id} for session {cmd.Session} will be removed");
					}
				}
			});
		}
	}
}