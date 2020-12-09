using TimeLab.Component;

namespace TimeLab.Command {
	public sealed class AddEntityComponentCommand : ILocationCommand {
		public readonly ulong      Entity;
		public readonly IComponent Component;

		public AddEntityComponentCommand(ulong entity, IComponent component) {
			Entity    = entity;
			Component = component;
		}
	}
}