using TimeLab.Systems;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	public sealed class WorldUpdater : MonoBehaviour {
		UpdateSystem _system;

		[Inject]
		public void Init(UpdateSystem system) {
			_system = system;
		}

		void Update() => _system.Update(Time.deltaTime);
	}
}