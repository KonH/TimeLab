using FluentAssertions;
using NUnit.Framework;
using TimeLab.Shared;

namespace TimeLab.Tests {
	public sealed class TimeProviderTests {
		TimeProvider _provider;

		[SetUp]
		public void Init() {
			var settings = new TimeSettings(realStartTime: 1);
			_provider = new TimeProvider(settings);
		}

		[Test]
		public void IsRealTimeNotStartedBeforeStartTime() {
			_provider.IsRealTime.Should().BeFalse();
		}

		[Test]
		public void IsRealTimeStartedAfterStartTime() {
			_provider.Advance(1.1f);

			_provider.IsRealTime.Should().BeTrue();
		}
	}
}