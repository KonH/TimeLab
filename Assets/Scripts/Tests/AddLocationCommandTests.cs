using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Shared;
using TimeLab.Systems;
using TimeLab.ViewModel;

namespace TimeLab.Tests {
	public sealed class AddLocationCommandTests : TimeLabWorldTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
			Container.Resolve<AddLocationSystem>();
		}

		[Test]
		public void IsLocationAdded() {
			var recorder = Container.Resolve<WorldCommandRecorder>();
			var producer = Container.Resolve<WorldSignalProducer>();
			var world    = Container.Resolve<World>();
			var id       = 1ul;
			var bounds   = new Rect2DInt(1, 2, 3, 4);

			recorder.TryRecord(new AddLocationCommand(id, bounds));
			producer.Produce();

			world.Locations.Should().HaveCount(1);
			world.Locations.First().Id.Should().Be(id);
			world.Locations.First().Bounds.Should().Be(bounds);
		}
	}
}