using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Manager;
using TimeLab.Systems;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class AddEntityCommandTests : TimeLabLocationTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
			SubContainer.Resolve<AddEntitySystem>();
		}

		[Test]
		public void IsEntityAdded() {
			var recorder = SubContainer.Resolve<LocationCommandRecorder>();
			var updater  = SubContainer.Resolve<UpdateManager>();
			var location = SubContainer.Resolve<Location>();
			var id       = 2ul;
			var position = new Vector2Int(1, 2);

			recorder.TryRecord(new AddEntityCommand(id, position));
			updater.Update();

			location.Entities.Should().HaveCount(1);
			location.Entities.First().Id.Should().Be(id);
			location.Entities.First().Position.Value.Should().Be(position);
		}
	}
}