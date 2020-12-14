namespace TimeLab.Component {
	public sealed class RefillSource : IComponent {
		public readonly string Type;
		public readonly float  Decrease;

		public int Amount;

		public RefillSource(string type, float decrease, int amount) {
			Type     = type;
			Decrease = decrease;
			Amount   = amount;
		}
	}
}