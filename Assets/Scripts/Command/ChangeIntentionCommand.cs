namespace TimeLab.Command {
	public sealed class ChangeIntentionCommand : ILocationCommand {
		public readonly ulong  Target;
		public readonly string NewIntention;

		public ChangeIntentionCommand(ulong target, string newIntention) {
			Target       = target;
			NewIntention = newIntention;
		}

		public override string ToString() {
			return $"[{nameof(ChangeIntentionCommand)}] {nameof(Target)}: {Target}, {nameof(NewIntention)}: {NewIntention}";
		}
	}
}