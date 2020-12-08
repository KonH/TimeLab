using TimeLab.Shared;

namespace TimeLab.Command {
	public sealed class AddLocationCommand : WorldCommand {
		public readonly ulong     Id;
		public readonly Rect2DInt Bounds;

		public AddLocationCommand(ulong id, Rect2DInt bounds) {
			Id     = id;
			Bounds = bounds;
		}
	}
}