using System.Linq;
using TimeLab.Component;
using TimeLab.Manager;
using TimeLab.Shared;
using TimeLab.ViewModel;
using UnityEngine;

namespace TimeLab.Systems {
	public sealed class LocationRefillSystem : ILocationUpdater {
		readonly Timer _timer = new Timer(1);

		readonly Location _location;

		public LocationRefillSystem(Location location) {
			_location = location;
		}

		public void Update(float deltaTime) {
			if ( !_timer.Tick(deltaTime) ) {
				return;
			}
			var locationAreas = _location.Components
				.OfType<RefillArea>()
				.ToArray();
			foreach ( var entity in _location.Entities ) {
				foreach ( var area in locationAreas ) {
					var needs = entity.Components
						.OfType<CharacterNeed>()
						.Where(n => n.Type == area.Type);
					foreach ( var need in needs ) {
						need.Amount = Mathf.Max(need.Amount - area.Decrease, 0);
						Debug.Log($"Character {entity.Id} need '{need.Type}' refilled using location area");
					}
				}
			}
		}
	}
}