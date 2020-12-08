using UnityEngine;

namespace TimeLab.Command {
	[InputCommand]
	public sealed class MovePlayerCommand : IWorldCommand {
		public readonly ulong      Session;
		public readonly Vector2Int Direction;

		public MovePlayerCommand(ulong session, Vector2Int direction) {
			Session   = session;
			Direction = direction;
		}
	}
}