using UnityEngine;

namespace TimeLab.Command {
	public sealed class MovePlayerCommand : WorldCommand {
		public readonly ulong      Session;
		public readonly Vector2Int Direction;

		public MovePlayerCommand(ulong session, Vector2Int direction) {
			Session   = session;
			Direction = direction;
		}
	}
}