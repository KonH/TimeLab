using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Systems;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class MoveEntityCommandTests : TimeLabLocationTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
			SubContainer.Resolve<AddEntitySystem>();
			SubContainer.Resolve<MoveEntitySystem>();
		}

		[Test]
		public void IsEntityMoved() {
			var recorder = SubContainer.Resolve<LocationCommandRecorder>();
			var producer = SubContainer.Resolve<LocationSignalProducer>();
			var location = SubContainer.Resolve<Location>();
			var id       = 2ul;
			var position = new Vector2Int(1, 2);

			recorder.TryRecord(new AddEntityCommand(id, Vector2Int.zero));
			recorder.TryRecord(new MoveEntityCommand(id, position));
			producer.Produce();

			location.Entities.First().Position.Value.Should().Be(position);
		}
	}
}