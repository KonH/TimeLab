using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Tests {
	public sealed class SignalProducerTests : ZenjectUnitTestFixture {
		PermanentCommandQueue<ICommand> _queue;
		SignalProducer<ICommand>        _producer;

		bool _isProduced;

		[SetUp]
		public void Init() {
			SignalBusInstaller.Install(Container);
			Container.DeclareSignal<TestCommand>();
			var bus  = Container.Resolve<SignalBus>();
			bus.Subscribe<TestCommand>(() => _isProduced = true);
			var world = new World();
			_queue    = new PermanentCommandQueue<ICommand>();
			_producer = new SignalProducer<ICommand>(world, _queue, bus);
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