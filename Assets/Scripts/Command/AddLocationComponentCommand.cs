using TimeLab.Component;

namespace TimeLab.Command {
	public sealed class AddLocationComponentCommand : ILocationCommand {
		public readonly ulong      Location;
		public readonly IComponent Component;

		public AddLocationComponentCommand(ulong location, IComponent component) {
			Location  = location;
			Component = component;
		}
	}
}