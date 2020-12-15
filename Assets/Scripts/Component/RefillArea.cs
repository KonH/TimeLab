namespace TimeLab.Component {
	public sealed class RefillArea : IComponent {
		public readonly string Type;
		public readonly float  Decrease;

		public RefillArea(string type, float decrease) {
			Type   = type;
			Decrease = decrease;
		}

		public override string ToString() {
			return $"[{nameof(RefillArea)}] {nameof(Type)}: {Type}, {nameof(Decrease)}: {Decrease}";
		}
	}
}