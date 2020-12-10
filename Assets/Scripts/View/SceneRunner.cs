using TimeLab.Manager;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	public sealed class SceneRunner : MonoBehaviour {
		TimelineController _timelineController;

		[Inject]
		public void Init(TimelineController timelineController) {
			_timelineController = timelineController;
		}

		void Start() {
			_timelineController.Initialize();
		}
	}
}