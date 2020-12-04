using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Systems;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class MovePlayerCommandTests : TimeLabLocationTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
			Container.Resolve<MovePlayerSystem>();
			SubContainer.Resolve<AddEntitySystem>();
			SubContainer.Resolve<MoveEntitySystem>();
		}

		[Test]
		public void IsEntityWithPlayerComponentMoved() {
			var worldRecorder = Container.Resolve<WorldCommandRecorder>();
			var worldProducer = Container.Resolve<WorldSignalProducer>();
			var locRecorder   = SubContainer.Resolve<LocationCommandRecorder>();
			var locProducer   = SubContainer.Resolve<LocationSignalProducer>();
			var location      = SubContainer.Resolve<Location>();
			var id            = 2ul;
			var position      = new Vector2Int(1, 1);
			var direction     = Vector2Int.left;

			locRecorder.TryRecord(new AddEntityCommand(id, position, new PlayerComponent()));
			worldRecorder.TryRecord(new MovePlayerCommand(direction));
			locProducer.Produce();
			worldProducer.Produce();
			locProducer.Produce();

			location.Entities.First().Position.Value.Should().Be(position + direction);
		}
	}
}