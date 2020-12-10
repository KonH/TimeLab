using TimeLab.Command;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Manager {
	/// <summary>
	/// Operates with current game timeline
	/// </summary>
	public sealed class TimelineController {
		readonly Session              _session;
		readonly World                _world;
		readonly CommandStorage       _commandStorage;
		readonly WorldCommandRecorder _worldRecorder;
		readonly WorldGenerator       _worldGenerator;
		readonly UpdateManager        _updateManager;

		public TimelineController(
			Session session, World world, CommandStorage commandStorage, WorldCommandRecorder worldRecorder,
			WorldGenerator worldGenerator, UpdateManager updateManager) {
			_session        = session;
			_world          = world;
			_worldRecorder  = worldRecorder;
			_commandStorage = commandStorage;
			_worldGenerator = worldGenerator;
			_updateManager  = updateManager;
		}

		public void Initialize() {
			ApplyGeneration();
			if ( !_session.IsFirstRun ) {
				var newTime  = _session.NewTime;
				Debug.Log($"{nameof(TimelineController)}.{nameof(Initialize)}: time-skipping to {newTime}");
				var tickTime = 1 / 30.0f;
				while ( _world.Time.Current.Value < newTime ) {
					_updateManager.Update(tickTime);
				}
				AddCurrentPlayerForNewWorld();
				return;
			}
			AddPlayer();
			_session.IsFirstRun = false;
		}

		public void Travel(double offset) {
			RemovePlayerByTimeOfTransition();
			var newTime = _world.Time.Current.Value + offset;
			newTime = newTime > double.Epsilon ? newTime : double.Epsilon;
			Debug.Log($"{nameof(TimelineController)}.{nameof(Travel)}: travel to {newTime}");
			_session.NewTime = newTime;
		}

		void RemovePlayerByTimeOfTransition() {
			_commandStorage.GetWorldCommands().Enqueue(
				_world.Time.Current.Value, new RemovePlayerCommand(_session.Id));
		}

		void ApplyGeneration() {
			var generationCommands = _worldGenerator.Generate();
			_commandStorage.Reset(generationCommands);
		}

		void AddCurrentPlayerForNewWorld() {
			_session.Id++;
			AddPlayer();
		}

		void AddPlayer() {
			_worldRecorder.Record(new AddPlayerCommand(_session.Id));
		}
	}
}