namespace TimeLab.Command {
	public sealed class ChangeIntentionCommand : ILocationCommand {
		public readonly ulong Source;

		public ChangeIntentionCommand(ulong source) {
			Source = source;
		}

		public override string ToString() {
			return $"[{nameof(ChangeIntentionCommand)}] {nameof(Source)}: {Source}";
		}
	}
}