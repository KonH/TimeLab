using NUnit.Framework;

namespace TimeLab.Tests {
	public sealed class LocationSystemTests : TimeLabLocationTestFixture {
		[SetUp]
		public override void Init() {}

		[Test]
		public void IsSetupValid() {
			base.Init();
		}
	}
}