using TimeLab.Component;
using TimeLab.Shared;
using UniRx;

namespace TimeLab.ViewModel {
	/// <summary>
	/// State for specific location
	/// </summary>
	public sealed class Location {
		public ulong                          Id         { get; }
		public Rect2DInt                      Bounds     { get; }
		public ReactiveCollection<Entity>     Entities   { get; } = new ReactiveCollection<Entity>();
		public ReactiveCollection<IComponent> Components { get; } = new ReactiveCollection<IComponent>();
		public LocationPortal                 Portal     { get; } = new LocationPortal();

		public Location(ulong id, Rect2DInt bounds) {
			Id     = id;
			Bounds = bounds;
		}
	}
}