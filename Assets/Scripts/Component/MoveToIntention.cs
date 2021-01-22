namespace TimeLab.Component {
	public sealed class MoveToIntention : IIntention {
		public readonly ulong TargetEntity;

		public MoveToIntention(ulong targetEntity) {
			TargetEntity = targetEntity;
		}

		public override string ToString() {
			return $"[{nameof(MoveToIntention)}] {nameof(TargetEntity)}: {TargetEntity}";
		}
	}
}