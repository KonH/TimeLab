using TimeLab.Manager;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	public sealed class WorldUpdater : MonoBehaviour {
		UpdateManager _manager;

		[Inject]
		public void Init(UpdateManager manager) {
			_manager = manager;
		}

		void Update() => _manager.Update(Time.deltaTime);
	}
}