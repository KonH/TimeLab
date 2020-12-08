using UnityEngine;

namespace TimeLab.Command {
	public sealed class MoveEntityCommand : LocationCommand {
		public readonly ulong      Id;
		public readonly Vector2Int Position;

		public MoveEntityCommand(ulong id, Vector2Int position) {
			Id       = id;
			Position = position;
		}
	}
}