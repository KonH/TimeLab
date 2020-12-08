using TimeLab.Command;

namespace TimeLab.Tests {
	sealed class TestCommand : ICommand {
		public bool IsHistory { get; set; }

		public TestCommand(bool isHistory = false) {
			IsHistory = isHistory;
		}
	}
}