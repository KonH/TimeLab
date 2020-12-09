using System.Linq;
using Zenject;

namespace TimeLab.Tests {
	public static class TestExtensions {
		public static void ResolveSystems<T>(this DiContainer container) {
			var assembly = typeof(T).Assembly;
			var types = assembly.GetTypes()
				.Where(t => (t != typeof(T)) && typeof(T).IsAssignableFrom(t));
			foreach ( var type in types ) {
				container.Resolve(type);
			}
		}
	}
}