using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Shared;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	/// <summary>
	/// Operates with current game timeline
	/// </summary>
	public sealed class TimelineController {
		readonly TimeSettings   _timeSettings;
		readonly TimeProvider   _timeProvider;
		readonly CommandStorage _commandStorage;
		readonly World          _world;
		readonly Session        _session;
		readonly IdGenerator    _idGenerator;

		public TimelineController(
			TimeSettings timeSettings, TimeProvider timeProvider, CommandStorage commandStorage,
			World world, Session session, IdGenerator idGenerator) {
			_timeSettings   = timeSettings;
			_timeProvider   = timeProvider;
			_commandStorage = commandStorage;
			_world          = world;
			_session        = session;
			_idGenerator    = idGenerator;
		}

		public void Restart() {
			_timeSettings.Reset(0);
			_commandStorage.Clear();
		}

		public void RestartWithReplay() {
			_timeSettings.Reset(_timeProvider.CurrentTime);
			_commandStorage.Reset();
		}

		public void TravelBackward() {
			_timeSettings.Reset(_timeProvider.CurrentTime);
			_commandStorage.Reset();
			AddCurrentPlayerForNewWorld();
		}

		void AddCurrentPlayerForNewWorld() {
			var (location, player) = _world.Locations
				.Select(l => (location: l, players: l.Entities
					.Where(e => e.Components
						.OfType<PlayerComponent>()
						.Any(c => c.Session == _session.Id))
					.ToArray()))
				.Where(p => (p.players.Length > 0))
				.Select(p => (p.location, p.players.First()))
				.First();
			_session.Id++;
			var locationCommands = _commandStorage.GetLocationCommands(location.Id);
			var newComponents    = player.Components.Where(c => !(c is PlayerComponent)).ToList();
			newComponents.Add(new PlayerComponent(_session.Id));
			locationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(), player.Position.Value, newComponents.ToArray()));
		}
	}
}