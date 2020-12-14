namespace TimeLab.Component {
	public sealed class CharacterNeed : IComponent {
		public readonly string Type;

		public float Amount;

		public CharacterNeed(string type, float amount) {
			Type   = type;
			Amount = amount;
		}

		public override string ToString() {
			return $"[{nameof(CharacterNeed)}] {nameof(Type)}: {Type}, {nameof(Amount)}: {Amount}";
		}
	}
}