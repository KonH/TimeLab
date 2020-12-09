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
	public sealed class AddComponentCommandTests : TimeLabLocationTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
			SubContainer.Resolve<AddEntitySystem>();
			SubContainer.Resolve<AddComponentSystem>();
		}

		[Test]
		public void IsEntityAdded() {
			var recorder = SubContainer.Resolve<LocationCommandRecorder>();
			var updater  = SubContainer.Resolve<UpdateManager>();
			var location = SubContainer.Resolve<Location>();
			var id       = 2ul;

			recorder.Record(new AddEntityCommand(id, Vector2Int.zero));
			recorder.Record(new AddComponentCommand(id, new RenderComponent(string.Empty)));
			updater.Update();

			location.Entities.First().Components.Should().Contain(c => c is RenderComponent);
		}
	}
}