namespace TimeLab.Command {
	[InputCommand]
	public sealed class AddPlayerCommand : IWorldCommand {
		public readonly ulong Session;

		public AddPlayerCommand(ulong session) {
			Session = session;
		}

		public override string ToString() {
			return $"[{nameof(AddPlayerCommand)}] {nameof(Session)}: {Session}";
		}
	}
}