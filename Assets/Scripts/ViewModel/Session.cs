namespace TimeLab.ViewModel {
	public sealed class Session {
		public ulong Id         { get; set; }
		public bool  IsFirstRun { get; set; } = true;
	}
}