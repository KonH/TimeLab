using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Systems;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class AddEntityComponentCommandTests : TimeLabLocationTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
		}

		[Test]
		public void IsEntityAdded() {
			var recorder = SubContainer.Resolve<LocationCommandRecorder>();
			var updater  = SubContainer.Resolve<UpdateManager>();
			var location = SubContainer.Resolve<Location>();
			var id       = 2ul;

			recorder.Record(new AddEntityCommand(id, Vector2Int.zero));
			recorder.Record(new AddEntityComponentCommand(id, new RenderComponent(string.Empty)));
			updater.Update();

			location.Entities.First().Components.Should().Contain(c => c is RenderComponent);
		}
	}
}