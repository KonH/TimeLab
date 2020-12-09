using TimeLab.Component;

namespace TimeLab.Command {
	public sealed class AddComponentCommand : ILocationCommand {
		public readonly ulong      Entity;
		public readonly IComponent Component;

		public AddComponentCommand(ulong entity, IComponent component) {
			Entity    = entity;
			Component = component;
		}
	}
}