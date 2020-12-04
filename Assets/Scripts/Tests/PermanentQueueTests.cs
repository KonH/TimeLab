using System;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Shared;

namespace TimeLab.Tests {
	public sealed class PermanentQueueTests {
		PermanentQueue<int> _queue;

		[SetUp]
		public void Init() {
			_queue = new PermanentQueue<int>();
		}

		[Test]
		public void IsElementQueued() {
			_queue.Enqueue(0, 1);

			_queue.Count.Should().Be(1);
		}

		[Test]
		public void IsElementFailedToQueuedIfNewTimeIsLess() {
			_queue.Enqueue(0, 1);

			Assert.Throws<InvalidOperationException>(() => _queue.Enqueue(-1, 1));
		}

		[Test]
		public void IsElementDequeued() {
			var element = 1;
			_queue.Enqueue(0, element);

			_queue.TryDequeue(0, out var content).Should().BeTrue();
			content.Should().Be(element);
		}

		[Test]
		public void IsElementIsNotDequeuedIfTimeIsLess() {
			_queue.Enqueue(1, 1);

			_queue.TryDequeue(0, out _).Should().BeFalse();
		}

		[Test]
		public void IsElementIsNotDequeuedTwice() {
			_queue.Enqueue(0, 1);

			_queue.TryDequeue(0, out _);

			_queue.TryDequeue(0, out _).Should().BeFalse();
		}
	}
}