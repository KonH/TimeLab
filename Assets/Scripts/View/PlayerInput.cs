using System.Collections.Generic;
using TimeLab.Command;
using TimeLab.Manager;
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

		WorldCommandRecorder _recorder;

		[Inject]
		public void Init(WorldCommandRecorder recorder) {
			_recorder = recorder;
		}

		void Update() {
			var direction = GetDirection();
			if ( direction != Vector2Int.zero ) {
				_recorder.TryRecord(new MovePlayerCommand(direction));
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