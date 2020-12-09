namespace TimeLab.Command {
	[InputCommand]
	public sealed class RemovePlayerCommand : IWorldCommand {
		public readonly ulong Session;

		public RemovePlayerCommand(ulong session) {
			Session = session;
		}
	}
}