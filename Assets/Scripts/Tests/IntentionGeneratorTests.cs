using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Tests {
	public sealed class IntentionGeneratorTests : TimeLabLocationTestFixture {
		const string MainType  = "Hunger";
		const string OtherType = "Stress";

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
			_recorder.Record(new AddEntityComponentCommand(_entityId, new CharacterNeed(MainType, 1.5f)));
			_recorder.Record(new AddEntityComponentCommand(_entityId, new CharacterNeed(OtherType, 1)));
			_recorder.Record(new AddEntityComponentCommand(_entityId, new AIComponent()));
			_updater.Update();
		}

		[Test]
		public void IsIntentionChanged() {
			var ai           = GetAIComponent();
			var initialValue = ai.Intention;

			_updater.Update(10);

			var updatedValue = ai.Intention;
			updatedValue.Should().NotBe(initialValue);
		}

		[Test]
		public void IsSignalProduced() {
			var isProduced = false;
			var bus        = SubContainer.Resolve<SignalBus>();
			bus.Subscribe<ChangeIntentionCommand>(e => isProduced = true);

			_updater.Update(10);
			_updater.Update();

			isProduced.Should().BeTrue();
		}

		AIComponent GetAIComponent() =>
			_world.Locations.First()
				.Entities.Single(e => e.Id == _entityId)
				.Components.OfType<AIComponent>().Single();
	}
}