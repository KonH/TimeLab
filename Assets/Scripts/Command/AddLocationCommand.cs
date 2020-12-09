using TimeLab.Shared;

namespace TimeLab.Command {
	public sealed class AddLocationCommand : IWorldCommand {
		public readonly ulong     Id;
		public readonly Rect2DInt Bounds;

		public AddLocationCommand(ulong id, Rect2DInt bounds) {
			Id     = id;
			Bounds = bounds;
		}

		public override string ToString() {
			return $"[{nameof(AddLocationCommand)}] {nameof(Id)}: {Id}, {nameof(Bounds)}: {Bounds}";
		}
	}
}