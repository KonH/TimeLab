namespace TimeLab.Component {
	public sealed class AIComponent : IComponent {
		public IIntention Intention;

		public override string ToString() {
			return $"[{nameof(AIComponent)}] {nameof(Intention)}: {Intention}";
		}
	}
}