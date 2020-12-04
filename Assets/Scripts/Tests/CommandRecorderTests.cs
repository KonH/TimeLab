using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Shared;

namespace TimeLab.Tests {
	public sealed class CommandRecorderTests {
		TimeProvider         _time;
		PermanentQueue<int>  _queue;
		CommandRecorder<int> _recorder;

		[SetUp]
		public void Init() {
			_time     = new TimeProvider(new TimeSettings(1));
			_queue    = new PermanentQueue<int>();
			_recorder = new CommandRecorder<int>(_time, _queue);
		}

		[Test]
		public void IsCommandNotRecordedBeforeStartTime() {
			var result = _recorder.TryRecord(1);

			result.Should().BeFalse();
			_queue.Count.Should().Be(0);
		}

		[Test]
		public void IsCommandRecordedAfterStartTime() {
			_time.Advance(1);

			var result = _recorder.TryRecord(1);

			result.Should().BeTrue();
			_queue.Count.Should().Be(1);
		}
	}
}