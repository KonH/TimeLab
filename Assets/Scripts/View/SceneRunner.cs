using TimeLab.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
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

		[ContextMenu(nameof(TravelBackward))]
		public void TravelBackward() {
			_timelineController.Travel(-10);
			SceneManager.LoadScene(0);
		}

		[ContextMenu(nameof(TravelForward))]
		public void TravelForward() {
			_timelineController.Travel(10);
			SceneManager.LoadScene(0);
		}
	}
}