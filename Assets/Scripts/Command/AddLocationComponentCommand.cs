using TimeLab.Component;

namespace TimeLab.Command {
	public sealed class AddLocationComponentCommand : IWorldCommand {
		public readonly ulong      Location;
		public readonly IComponent Component;

		public AddLocationComponentCommand(ulong location, IComponent component) {
			Location  = location;
			Component = component;
		}

		public override string ToString() {
			return $"[{nameof(AddLocationComponentCommand)}] {nameof(Location)}: {Location}, {nameof(Component)}: {Component}";
		}
	}
}