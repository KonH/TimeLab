using System;
using TimeLab.Component;
using UniRx;
using UnityEngine;

namespace TimeLab.ViewModel {
	/// <summary>
	/// State for single entity
	/// </summary>
	public sealed class Entity : IEquatable<Entity> {
		public ulong                          Id         { get; }
		public ReactiveProperty<Vector2Int>   Position   { get; } = new ReactiveProperty<Vector2Int>();
		public ReactiveCollection<IComponent> Components { get; } = new ReactiveCollection<IComponent>();

		public Entity(ulong id) {
			Id = id;
		}

		public override int GetHashCode() => Id.GetHashCode();

		public bool Equals(Entity other) {
			if ( ReferenceEquals(null, other) ) {
				return false;
			}
			if ( ReferenceEquals(this, other) ) {
				return true;
			}
			return Id == other.Id;
		}

		public override bool Equals(object obj) {
			return ReferenceEquals(this, obj) || obj is Entity other && Equals(other);
		}

		public static bool operator ==(Entity left, Entity right) {
			return Equals(left, right);
		}

		public static bool operator !=(Entity left, Entity right) {
			return !Equals(left, right);
		}
	}
}