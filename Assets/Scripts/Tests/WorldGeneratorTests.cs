using NUnit.Framework;
using TimeLab.DI;
using TimeLab.Manager;
using TimeLab.Systems;
using TimeLab.ViewModel;

namespace TimeLab.Tests {
	public sealed class WorldGeneratorTests : TimeLabWorldTestFixture {
		[SetUp]
		public override void Init() {
			base.Init();
		}

		[Test]
		public void IsCreatedWithoutErrors() {
			var idGenerator = new IdGenerator();
			var generator   = new WorldGenerator(idGenerator);
			var storage     = generator.Generate();
			Container.Resolve<CommandStorage>().Reset(storage);

			var world   = Container.Resolve<World>();
			var holder  = Container.Resolve<LocationContainerHolder>();
			var updater = Container.Resolve<UpdateManager>();

			updater.Update();

			foreach ( var location in world.Locations ) {
				var subContainer = Container.CreateSubContainer();
				subContainer.Install<LocationInstaller>(new object[] { location, holder });
				subContainer.ResolveSystems<ILocationSystem>();
			}
			updater.Update();
		}
	}
}