using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class StressUseCaseTests : TimeLabLocationTestFixture {
		const string Type = "Stress";

		World                   _world;
		UpdateManager           _updater;
		LocationCommandRecorder _recorder;

		ulong _entityId;

		[SetUp]
		public override void Init() {
			base.Init();
			var generator = Container.Resolve<IdGenerator>();
			_world    = Container.Resolve<World>();
			_updater  = Container.Resolve<UpdateManager>();
			_recorder = SubContainer.Resolve<LocationCommandRecorder>();
			_entityId = generator.GetNextId();
			_recorder.Record(new AddEntityCommand(_entityId, Vector2Int.zero));
			_recorder.Record(new AddEntityComponentCommand(_entityId, new CharacterNeed(Type, 1)));
			_updater.Update();
		}

		[Test]
		public void StressIncreasesWithTime() {
			var need         = GetNeed();
			var initialValue = need.Amount;

			_updater.Update(10);

			var updatedValue = need.Amount;
			updatedValue.Should().BeGreaterThan(initialValue);
		}

		[Test]
		public void IsStandInStressRefillAreaDecreaseStress() {
			var need         = GetNeed();
			var initialValue = need.Amount;
			AddRefillArea();

			_recorder.Record(new MoveEntityCommand(_entityId, Vector2Int.one));
			_updater.Update(10);

			var updatedValue = need.Amount;
			updatedValue.Should().BeLessThan(initialValue);
		}

		CharacterNeed GetNeed() =>
			_world.Locations.First()
				.Entities.Single(e => e.Id == _entityId)
				.Components.OfType<CharacterNeed>().Single();

		void AddRefillArea() {
			var recorder = Container.Resolve<WorldCommandRecorder>();
			recorder.Record(new AddLocationComponentCommand(_world.Locations.First().Id, new RefillArea(Type, 1)));
			_updater.Update();
		}
	}
}