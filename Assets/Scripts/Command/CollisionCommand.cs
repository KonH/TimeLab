namespace TimeLab.Command {
	public sealed class CollisionCommand : ILocationCommand {
		public readonly ulong Source;
		public readonly ulong Target;

		public CollisionCommand(ulong source, ulong target) {
			Source = source;
			Target = target;
		}
	}
}