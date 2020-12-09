using TimeLab.Command;
using TimeLab.ViewModel;

namespace TimeLab.Manager {
	/// <summary>
	/// Operates with current game timeline
	/// </summary>
	public sealed class TimelineController {
		readonly World          _world;
		readonly CommandStorage _commandStorage;
		readonly Session        _session;
		readonly IdGenerator    _idGenerator;
		readonly WorldGenerator _worldGenerator;

		public TimelineController(
			World world, CommandStorage commandStorage, Session session,
			IdGenerator idGenerator, WorldGenerator worldGenerator) {
			_world          = world;
			_commandStorage = commandStorage;
			_session        = session;
			_idGenerator    = idGenerator;
			_worldGenerator = worldGenerator;
		}

		public void Initialize() {
			if ( !_session.IsFirstRun ) {
				return;
			}
			_worldGenerator.Generate();
			AddPlayer(double.Epsilon);
			_session.IsFirstRun = false;
		}

		public void Travel(double offset) {
			RemovePlayerByTimeOfTransition();
			var newTime = _world.Time.Current.Value + offset;
			newTime = newTime > double.Epsilon ? newTime : double.Epsilon;
			_world.Time.Current.Value = 0;
			_commandStorage.Reset();
			_worldGenerator.Generate();
			AddCurrentPlayerForNewWorld(newTime);
		}

		void RemovePlayerByTimeOfTransition() {
			_commandStorage.GetWorldCommands().Enqueue(
				_world.Time.Current.Value, new RemovePlayerCommand(_session.Id));
		}

		void AddCurrentPlayerForNewWorld(double newTime) {
			_session.Id++;
			AddPlayer(newTime);
		}

		void AddPlayer(double time) {
			var commands = _commandStorage.GetWorldCommands();
			commands.Enqueue(time, new AddPlayerCommand(
				_idGenerator.GetNextId(), _session.Id));
		}
	}
}