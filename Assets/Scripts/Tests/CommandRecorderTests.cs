using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.Shared;

namespace TimeLab.Tests {
	public sealed class CommandRecorderTests {
		TimeProvider                    _time;
		PermanentCommandQueue<ICommand> _queue;
		CommandRecorder<ICommand>       _recorder;

		[SetUp]
		public void Init() {
			_time     = new TimeProvider(new TimeSettings(1));
			_queue    = new PermanentCommandQueue<ICommand>();
			_recorder = new CommandRecorder<ICommand>(_time, _queue);
		}

		[Test]
		public void IsCommandNotRecordedBasedOnHistoryCommand() {
			var result = _recorder.TryRecord(new TestCommand(true), new TestCommand());

			result.Should().BeFalse();
			_queue.Count.Should().Be(0);
		}

		[Test]
		public void IsCommandRecordedBasedOnNormalCommand() {
			_time.Advance(1);

			var result = _recorder.TryRecord(new TestCommand(), new TestCommand());

			result.Should().BeTrue();
			_queue.Count.Should().Be(1);
		}

		[Test]
		public void IsCommandRecordedBasedOnNoCommand() {
			_time.Advance(1);

			var result = _recorder.TryRecord(null, new TestCommand());

			result.Should().BeTrue();
			_queue.Count.Should().Be(1);
		}
	}
}