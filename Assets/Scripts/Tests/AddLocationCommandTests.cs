using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.Systems;
using TimeLab.ViewModel;

namespace TimeLab.Tests {
	public sealed class AddLocationCommandTests : TimeLabWorldTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
		}

		[Test]
		public void IsLocationAdded() {
			var recorder = Container.Resolve<WorldCommandRecorder>();
			var updater  = Container.Resolve<UpdateManager>();
			var world    = Container.Resolve<World>();
			var id       = 1ul;
			var bounds   = new Rect2DInt(1, 2, 3, 4);

			recorder.Record(new AddLocationCommand(id, bounds));
			updater.Update();

			world.Locations.Should().HaveCount(1);
			world.Locations.First().Id.Should().Be(id);
			world.Locations.First().Bounds.Should().Be(bounds);
		}
	}
}