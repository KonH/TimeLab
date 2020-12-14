using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Tests {
	public sealed class FoodUseCaseTests : TimeLabLocationTestFixture {
		const string Type = "Hunger";

		World                   _world;
		UpdateManager           _updater;
		LocationCommandRecorder _recorder;

		ulong _entityId;
		ulong _sourceId;

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
			_sourceId = generator.GetNextId();
			_recorder.Record(new AddEntityCommand(_sourceId, Vector2Int.one));
			_recorder.Record(new AddEntityComponentCommand(_sourceId, new RefillSource(Type, 1, 1)));
			_updater.Update();
		}

		[Test]
		public void HungerIncreasesWithTime() {
			var need         = GetNeed();
			var initialValue = need.Amount;

			_updater.Update(10);

			var updatedValue = need.Amount;
			updatedValue.Should().BeGreaterThan(initialValue);
		}

		[Test]
		public void IsInteractionWithFoodSourceDecreaseHunger() {
			var need         = GetNeed();
			var initialValue = need.Amount;

			_recorder.Record(new MoveEntityCommand(_entityId, Vector2Int.one));
			_updater.Update();

			var updatedValue = need.Amount;
			updatedValue.Should().BeLessThan(initialValue);
		}

		[Test]
		public void IsInteractionWithFoodSourceDecreaseRemainingAmount() {
			var source       = GetSource();
			var initialValue = source.Amount;

			_recorder.Record(new MoveEntityCommand(_entityId, Vector2Int.one));
			_updater.Update();

			var updatedValue = source.Amount;
			updatedValue.Should().BeLessThan(initialValue);
		}

		[Test]
		public void InteractionWithEmptyFoodSourceNotAffectingHunger() {
			var need   = GetNeed();
			var source = GetSource();
			source.Amount = 0;
			var initialValue = need.Amount;

			_recorder.Record(new MoveEntityCommand(_entityId, Vector2Int.one));
			_updater.Update();

			var updatedValue = need.Amount;
			updatedValue.Should().Be(initialValue);
		}

		CharacterNeed GetNeed() =>
			_world.Locations.First()
				.Entities.Single(e => e.Id == _entityId)
				.Components.OfType<CharacterNeed>().Single();

		RefillSource GetSource() =>
			_world.Locations.First()
				.Entities.Single(e => e.Id == _sourceId)
				.Components.OfType<RefillSource>().Single();
	}
}