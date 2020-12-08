namespace TimeLab.Command {
	/// <summary>
	/// Command, scoped to specific location
	/// </summary>
	public abstract class LocationCommand : ICommand {
		public bool IsHistory { get; set; }
	}
}