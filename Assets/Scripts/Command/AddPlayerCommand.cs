namespace TimeLab.Command {
	[InputCommand]
	public sealed class AddPlayerCommand : IWorldCommand {
		public readonly ulong Id;
		public readonly ulong Session;

		public AddPlayerCommand(ulong id, ulong session) {
			Id      = id;
			Session = session;
		}

		public override string ToString() {
			return $"[{nameof(AddPlayerCommand)}] {nameof(Id)}: {Id}, {nameof(Session)}: {Session}";
		}
	}
}