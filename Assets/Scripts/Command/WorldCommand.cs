namespace TimeLab.Command {
	/// <summary>
	/// Global command
	/// </summary>
	public abstract class WorldCommand : ICommand {
		public bool IsHistory { get; set; }
	}
}