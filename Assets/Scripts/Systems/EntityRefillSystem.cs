using System.Linq;
using TimeLab.Command;
using TimeLab.Component;
using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.Systems {
	public sealed class EntityRefillSystem : ILocationSystem {
		public EntityRefillSystem(Location location, SignalBus bus) {
			bus.Subscribe<CollisionCommand>(cmd => {
				var source  = location.Entities.First(e => e.Id == cmd.Source);
				var target  = location.Entities.First(e => e.Id == cmd.Target);
				var needs   = source.Components.OfType<CharacterNeed>();
				foreach ( var need in needs ) {
					var refillSource = target.Components
						.OfType<RefillSource>()
						.FirstOrDefault(s => (s.Type == need.Type) && (s.Amount > 0));
					if ( refillSource == null ) {
						continue;
					}
					need.Amount = Mathf.Max(need.Amount - refillSource.Decrease, 0);
					Debug.Log($"Character {source.Id} need '{need.Type}' refilled using {target.Id}");
					refillSource.Amount--;
				}
			});
		}
	}
}