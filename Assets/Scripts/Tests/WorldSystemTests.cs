using NUnit.Framework;

namespace TimeLab.Tests {
	public sealed class WorldSystemTests : TimeLabWorldTestFixture {
		[SetUp]
		public override void Init() {}

		[Test]
		public void IsSetupValid() {
			base.Init();
		}
	}
}