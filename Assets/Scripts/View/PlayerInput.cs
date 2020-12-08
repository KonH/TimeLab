using System.Collections.Generic;
using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	/// <summary>
	/// Translates keyboard inputs into move player command
	/// </summary>
	public sealed class PlayerInput : MonoBehaviour {
		readonly Dictionary<KeyCode, Vector2Int> _directions = new Dictionary<KeyCode, Vector2Int> {
			[KeyCode.LeftArrow]  = Vector2Int.left,
			[KeyCode.RightArrow] = Vector2Int.right,
			[KeyCode.UpArrow]    = Vector2Int.up,
			[KeyCode.DownArrow]  = Vector2Int.down
		};

		Session              _session;
		WorldCommandRecorder _recorder;

		[Inject]
		public void Init(Session session, WorldCommandRecorder recorder) {
			_session  = session;
			_recorder = recorder;
		}

		void Update() {
			var direction = GetDirection();
			if ( direction != Vector2Int.zero ) {
				_recorder.TryRecord(new MovePlayerCommand(_session.Id, direction));
			}
		}

		Vector2Int GetDirection() {
			foreach ( var pair in _directions ) {
				if ( Input.GetKeyDown(pair.Key) ) {
					return pair.Value;
				}
			}
			return Vector2Int.zero;
		}
	}
}