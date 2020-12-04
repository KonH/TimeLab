using UniRx;

namespace TimeLab.ViewModel {
	/// <summary>
	/// Current game state
	/// </summary>
	public sealed class World {
		public ReactiveCollection<Location> Locations { get; } = new ReactiveCollection<Location>();
	}
}