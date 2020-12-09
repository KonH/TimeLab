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
	public sealed class MovePlayerCommandTests : TimeLabLocationTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
		}

		[Test]
		public void IsEntityWithPlayerComponentMoved() {
			var session       = Container.Resolve<Session>();
			var worldRecorder = Container.Resolve<WorldCommandRecorder>();
			var locRecorder   = SubContainer.Resolve<LocationCommandRecorder>();
			var updater       = SubContainer.Resolve<UpdateManager>();
			var location      = SubContainer.Resolve<Location>();
			var id            = 2ul;
			var position      = new Vector2Int(1, 1);
			var direction     = Vector2Int.left;

			locRecorder.Record(new AddEntityCommand(id, position));
			locRecorder.Record(new AddEntityComponentCommand(id, new PlayerComponent(session.Id)));
			updater.Update();
			worldRecorder.Record(new MovePlayerCommand(session.Id, direction));
			updater.Update();

			location.Entities.First().Position.Value.Should().Be(position + direction);
		}

		[Test]
		public void IsEntityWithPlayerComponentNotMovedWithDifferentSession() {
			var session       = Container.Resolve<Session>();
			var worldRecorder = Container.Resolve<WorldCommandRecorder>();
			var locRecorder   = SubContainer.Resolve<LocationCommandRecorder>();
			var updater       = SubContainer.Resolve<UpdateManager>();
			var location      = SubContainer.Resolve<Location>();
			var id            = 2ul;
			var position      = new Vector2Int(1, 1);
			var direction     = Vector2Int.left;

			locRecorder.Record(new AddEntityCommand(id, position));
			locRecorder.Record(new AddEntityComponentCommand(id, new PlayerComponent(session.Id + 1)));
			updater.Update();
			worldRecorder.Record(new MovePlayerCommand(session.Id, direction));
			updater.Update();

			location.Entities.First().Position.Value.Should().Be(position);
		}
	}
}