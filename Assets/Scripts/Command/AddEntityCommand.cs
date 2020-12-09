using UnityEngine;

namespace TimeLab.Command {
	public sealed class AddEntityCommand : ILocationCommand {
		public readonly ulong      Id;
		public readonly Vector2Int Position;

		public AddEntityCommand(ulong id, Vector2Int position) {
			Id       = id;
			Position = position;
		}

		public override string ToString() {
			return $"[{nameof(AddEntityCommand)}] {nameof(Id)}: {Id}, {nameof(Position)}: {Position}";
		}
	}
}