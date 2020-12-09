using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Systems {
	public sealed class MovePlayerSystem : IWorldSystem {
		public MovePlayerSystem(World world, SignalBus bus, LocationContainerHolder holder) {
			bus.Subscribe<MovePlayerCommand>(cmd => {
				foreach ( var location in world.Locations ) {
					var players = location
						.Entities
						.Where(e => e.Components
							.Any(c => (c is PlayerComponent player) && (player.Session == cmd.Session)));
					foreach ( var player in players ) {
						var recorder = holder.Resolve<LocationCommandRecorder>(location.Id);
						recorder.Record(new MoveEntityCommand(player.Id, player.Position.Value + cmd.Direction));
					}
				}
			});
		}
	}
}