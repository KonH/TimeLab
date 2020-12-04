using UnityEngine;

namespace TimeLab.Command {
	public sealed class MovePlayerCommand : IWorldCommand {
		public readonly Vector2Int Direction;

		public MovePlayerCommand(Vector2Int direction) {
			Direction = direction;
		}
	}
}