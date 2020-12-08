using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.Shared;
using Zenject;

namespace TimeLab.Tests {
	public sealed class SignalProducerTests : ZenjectUnitTestFixture {
		PermanentCommandQueue<ICommand> _queue;
		SignalProducer<ICommand>        _producer;

		bool _isProduced;

		[SetUp]
		public void Init() {
			SignalBusInstaller.Install(Container);
			Container.DeclareSignal<string>();
			var bus  = Container.Resolve<SignalBus>();
			bus.Subscribe<string>(() => _isProduced = true);
			var time = new TimeProvider(new TimeSettings(0));
			_queue    = new PermanentCommandQueue<ICommand>();
			_producer = new SignalProducer<ICommand>(time, _queue, bus);
		}

		[Test]
		public void IsSignalProducedIfTimeReached() {
			_queue.Enqueue(0, new TestCommand());

			_producer.Produce();

			_isProduced.Should().BeTrue();
		}

		[Test]
		public void IsSignalIsNotProducedIfTimeNotReached() {
			_queue.Enqueue(1, new TestCommand());

			_producer.Produce();

			_isProduced.Should().BeFalse();
		}
	}
}