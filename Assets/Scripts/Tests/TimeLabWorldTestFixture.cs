using NUnit.Framework;
using TimeLab.DI;
using TimeLab.Systems;
using Zenject;

namespace TimeLab.Tests {
	public abstract class TimeLabWorldTestFixture : ZenjectUnitTestFixture {
		[SetUp]
		public virtual void Init() {
			Container.Install<GlobalInstaller>();
			Container.Install<WorldInstaller>();
			Container.ResolveSystems<IWorldSystem>();
		}
	}
}