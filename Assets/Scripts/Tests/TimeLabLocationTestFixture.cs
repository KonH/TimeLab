using System.Linq;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.DI;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.Systems;
using TimeLab.ViewModel;
using Zenject;

namespace TimeLab.Tests {
	public abstract class TimeLabLocationTestFixture : TimeLabWorldTestFixture {
		protected DiContainer SubContainer { get; private set; }

		[SetUp]
		public override void Init() {
			base.Init();
			SubContainer = CreateLocation();
		}

		protected DiContainer CreateLocation(ulong id = 1ul) {
			Container.Resolve<AddLocationSystem>();
			var recorder = Container.Resolve<WorldCommandRecorder>();
			var updater  = Container.Resolve<UpdateManager>();
			var bounds   = new Rect2DInt(1, 2, 3, 4);

			recorder.Record(new AddLocationCommand(id, bounds));
			updater.Update();

			var subContainer = Container.CreateSubContainer();
			var world  = Container.Resolve<World>();
			var holder = Container.Resolve<LocationContainerHolder>();
			subContainer.Install<LocationInstaller>(new object[] { world.Locations.First(l => l.Id == id), holder });
			return subContainer;
		}
	}
}