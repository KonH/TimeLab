using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class AIUseCaseTests : TimeLabLocationTestFixture {
		const string Type = "Hunger";

		IdGenerator             _generator;
		World                   _world;
		UpdateManager           _updater;
		LocationCommandRecorder _recorder;

		ulong _entityId;

		[SetUp]
		public override void Init() {
			base.Init();
			_generator = Container.Resolve<IdGenerator>();
			_world    = Container.Resolve<World>();
			_updater  = Container.Resolve<UpdateManager>();
			_recorder = SubContainer.Resolve<LocationCommandRecorder>();
			_entityId = _generator.GetNextId();
			_recorder.Record(new AddEntityCommand(_entityId, Vector2Int.zero));
			_recorder.Record(new AddEntityComponentCommand(_entityId, new CharacterNeed(Type, 1)));
			_recorder.Record(new AddEntityComponentCommand(_entityId, new AIComponent()));
		}

		[Test]
		public void IsBotMovedToRefillSource() {
			var sourceId = _generator.GetNextId();
			_recorder.Record(new AddEntityCommand(sourceId, Vector2Int.one));
			_recorder.Record(new AddEntityComponentCommand(sourceId, new RefillSource(Type, 1, 1)));
			_updater.Update();

			_updater.Update(5);
			_updater.Update(5);

			GetEntity(_entityId).Position.Value.Should().Be(GetEntity(sourceId).Position.Value);
		}

		[Test]
		public void IsBotMovedToRefillArea() {
			var worldRecorder = Container.Resolve<WorldCommandRecorder>();
			var newLocationId = _generator.GetNextId();
			worldRecorder.Record(new AddLocationCommand(newLocationId, Rect2DInt.Zero));
			worldRecorder.Record(new AddLocationComponentCommand(newLocationId, new RefillArea(Type, 1)));
			var sourceDoorId = _generator.GetNextId();
			_recorder.Record(new AddEntityCommand(sourceDoorId, Vector2Int.one));
			_recorder.Record(new AddEntityComponentCommand(sourceDoorId, new PortalComponent(newLocationId, Vector2Int.zero)));
			_updater.Update();

			_updater.Update(5);
			_updater.Update(5);
			_updater.Update();

			_world.Locations.First(l => l.Id == newLocationId).Entities.Should().Contain(e => e.Id == _entityId);
		}

		[Test]
		public void IsBotMovedToDistantRefillSource() {
			var worldRecorder = Container.Resolve<WorldCommandRecorder>();
			var newLocationId = _generator.GetNextId();
			worldRecorder.Record(new AddLocationCommand(newLocationId, Rect2DInt.Zero));
			worldRecorder.Record(new AddLocationComponentCommand(newLocationId, new RefillArea(Type, 1)));
			var sourceDoorId = _generator.GetNextId();
			_recorder.Record(new AddEntityCommand(sourceDoorId, Vector2Int.one));
			_recorder.Record(new AddEntityComponentCommand(sourceDoorId, new PortalComponent(newLocationId, Vector2Int.zero)));
			var sourceId = _generator.GetNextId();
			var storage = Container.Resolve<CommandStorage>().GetLocationCommands(newLocationId);
			storage.Enqueue(0, new AddEntityCommand(sourceId, Vector2Int.one));
			storage.Enqueue(0, new AddEntityComponentCommand(sourceId, new RefillSource(Type, 1, 1)));
			_updater.Update();

			_updater.Update(5);
			_updater.Update(5);
			_updater.Update();

			_world.Locations.First(l => l.Id == newLocationId).Entities.Should().Contain(e => e.Id == _entityId);
		}

		Entity GetEntity(ulong id) =>
			_world.Locations.First()
				.Entities.Single(e => e.Id == id);
	}
}