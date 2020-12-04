using TimeLab.Component;
using UniRx;
using UnityEngine;

namespace TimeLab.ViewModel {
	/// <summary>
	/// State for single entity
	/// </summary>
	public sealed class Entity {
		public ulong                          Id         { get; }
		public ReactiveProperty<Vector2Int>   Position   { get; } = new ReactiveProperty<Vector2Int>();
		public ReactiveCollection<IComponent> Components { get; } = new ReactiveCollection<IComponent>();

		public Entity(ulong id) {
			Id = id;
		}
	}
}