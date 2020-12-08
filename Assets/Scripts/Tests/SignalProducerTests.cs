using FluentAssertions;
using NUnit.Framework;
using TimeLab.Manager;
using TimeLab.Shared;
using Zenject;

namespace TimeLab.Tests {
	public sealed class SignalProducerTests : ZenjectUnitTestFixture {
		PermanentQueue<string> _queue;
		SignalProducer<string> _producer;

		bool _isProduced;

		[SetUp]
		public void Init() {
			SignalBusInstaller.Install(Container);
			Container.DeclareSignal<string>();
			var bus  = Container.Resolve<SignalBus>();
			bus.Subscribe<string>(() => _isProduced = true);
			var time = new TimeProvider(new TimeSettings(0));
			_queue    = new PermanentQueue<string>();
			_producer = new SignalProducer<string>(time, _queue, bus);
		}

		[Test]
		public void IsSignalProducedIfTimeReached() {
			_queue.Enqueue(0, string.Empty);

			_producer.Produce();

			_isProduced.Should().BeTrue();
		}

		[Test]
		public void IsSignalIsNotProducedIfTimeNotReached() {
			_queue.Enqueue(1, string.Empty);

			_producer.Produce();

			_isProduced.Should().BeFalse();
		}
	}
}