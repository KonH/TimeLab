namespace TimeLab.Component {
	public sealed class RenderComponent : IComponent {
		public readonly string Type;

		public RenderComponent(string type) {
			Type = type;
		}

		public override string ToString() {
			return $"[{nameof(RenderComponent)}] {nameof(Type)}: {Type}";
		}
	}
}