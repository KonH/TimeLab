using NUnit.Framework;
using TimeLab.DI;
using Zenject;

namespace TimeLab.Tests {
	public abstract class TimeLabWorldTestFixture : ZenjectUnitTestFixture {
		[SetUp]
		public virtual void Init() {
			Container.Install<GlobalInstaller>();
			Container.Install<WorldInstaller>();
		}
	}
}