namespace TimeLab.Component {
	public sealed class PlayerComponent : IComponent {
		public readonly ulong Session;

		public PlayerComponent(ulong session) {
			Session = session;
		}

		public override string ToString() {
			return $"[{nameof(PlayerComponent)}] {nameof(Session)}: {Session}";
		}
	}
}