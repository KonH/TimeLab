using UnityEngine;

namespace TimeLab.Component {
	public sealed class PortalComponent : IComponent {
		public readonly ulong      TargetLocation;
		public readonly Vector2Int TargetPosition;

		public PortalComponent(ulong targetLocation, Vector2Int targetPosition) {
			TargetLocation = targetLocation;
			TargetPosition = targetPosition;
		}
	}
}