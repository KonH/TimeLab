namespace TimeLab.Command {
	public sealed class RemoveEntityCommand : ILocationCommand {
		public readonly ulong Id;

		public RemoveEntityCommand(ulong id) {
			Id = id;
		}
	}
}