namespace TimeLab.Component {
	public sealed class CharacterNeed : IComponent {
		public readonly string Type;

		public float Amount;

		public CharacterNeed(string type, float amount) {
			Type   = type;
			Amount = amount;
		}
	}
}