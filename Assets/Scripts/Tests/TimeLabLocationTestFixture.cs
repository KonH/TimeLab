using System.Linq;
using NUnit.Framework;
using TimeLab.Command;
using TimeLab.DI;
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
			CreateLocation();
			SubContainer = Container.CreateSubContainer();
			var world  = Container.Resolve<World>();
			var holder = Container.Resolve<LocationContainerHolder>();
			SubContainer.Install<LocationInstaller>(new object[] { world.Locations.First(), holder });
		}

		void CreateLocation() {
			Container.Resolve<AddLocationSystem>();
			var recorder = Container.Resolve<WorldCommandRecorder>();
			var producer = Container.Resolve<WorldSignalProducer>();
			var id       = 1ul;
			var bounds   = new Rect2DInt(1, 2, 3, 4);

			recorder.TryRecord(new AddLocationCommand(id, bounds));
			producer.Produce();
		}
	}
}