using TimeLab.ViewModel;

namespace TimeLab.Command {
	public sealed class CollisionCommand : ILocationCommand {
		public readonly Entity Source;
		public readonly Entity Target;

		public CollisionCommand(Entity source, Entity target) {
			Source = source;
			Target = target;
		}
	}
}