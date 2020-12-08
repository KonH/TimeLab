using TimeLab.Component;
using UnityEngine;

namespace TimeLab.Command {
	public sealed class AddEntityCommand : LocationCommand {
		public readonly ulong        Id;
		public readonly Vector2Int   Position;
		public readonly IComponent[] Components;

		public AddEntityCommand(ulong id, Vector2Int position, params IComponent[] components) {
			Id         = id;
			Position   = position;
			Components = components;
		}
	}
}