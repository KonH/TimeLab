using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Manager {
	/// <summary>
	/// Operates with current game timeline
	/// </summary>
	public sealed class TimelineController {
		readonly World          _world;
		readonly CommandStorage _commandStorage;
		readonly Session        _session;
		readonly WorldGenerator _worldGenerator;
		readonly UpdateManager  _updateManager;

		public TimelineController(
			World world, CommandStorage commandStorage, Session session,
			WorldGenerator worldGenerator, UpdateManager updateManager) {
			_world          = world;
			_commandStorage = commandStorage;
			_session        = session;
			_worldGenerator = worldGenerator;
			_updateManager  = updateManager;
		}

		public void Initialize() {
			_worldGenerator.Generate();
			if ( !_session.IsFirstRun ) {
				var newTime  = _session.NewTime;
				Debug.Log($"{nameof(TimelineController)}.{nameof(Initialize)}: time-skipping to {newTime}");
				var tickTime = 1 / 30.0f;
				while ( _world.Time.Current.Value < newTime ) {
					_updateManager.Update(tickTime);
				}
				AddCurrentPlayerForNewWorld(_world.Time.Current.Value);
				return;
			}
			AddPlayer(double.Epsilon);
			_session.IsFirstRun = false;
		}

		public void Travel(double offset) {
			RemovePlayerByTimeOfTransition();
			var newTime = _world.Time.Current.Value + offset;
			newTime = newTime > double.Epsilon ? newTime : double.Epsilon;
			Debug.Log($"{nameof(TimelineController)}.{nameof(Travel)}: travel to {newTime}");
			_commandStorage.Reset();
			_session.NewTime = newTime;
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
			commands.Enqueue(time, new AddPlayerCommand(_session.Id));
		}
	}
}