using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Systems;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class TransferTests : TimeLabLocationTestFixture {
		const ulong EntityId         = 2;
		const ulong SecondLocationId = 3;

		LocationCommandRecorder _recorder;
		World                   _world;
		UpdateManager            _updater;

		[SetUp]
		public override void Init() {
			base.Init();
			_recorder = SubContainer.Resolve<LocationCommandRecorder>();
			_recorder.Record(new AddEntityCommand(EntityId, Vector2Int.zero));
			var secondSubContainer = CreateLocation(SecondLocationId);
			secondSubContainer.Resolve<AddEntitySystem>();
			_recorder.Record(new AddEntityCommand(4, Vector2Int.one));
			_recorder.Record(new AddEntityComponentCommand(4, new PortalComponent(SecondLocationId, Vector2Int.zero)));
			_world   = Container.Resolve<World>();
			_updater = Container.Resolve<UpdateManager>();
			SubContainer.Resolve<PortalManager>();
		}

		[Test]
		public void IsPortalCollectionTransferEntityToAnotherLocation() {
			_world.Locations[0].Portal.Enqueue(SecondLocationId, new Entity(EntityId));
			_updater.Update();

			_world.Locations[1].Entities.Should().Contain(e => e.Id == EntityId);
		}

		[Test]
		public void IsCollisionWithPortalComponentLeadsToTransfer() {
			_recorder.Record(new MoveEntityCommand(2, Vector2Int.one));
			_updater.Update();

			_world.Locations[0].Portal.Entries.TryGetValue(SecondLocationId, out var queue).Should().BeTrue();
			queue?.Dequeue().Id.Should().Be(EntityId);
		}
	}
}