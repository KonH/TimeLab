using TimeLab.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TimeLab.View {
	public sealed class SceneRunner : MonoBehaviour {
		WorldGenerator     _generator;
		TimelineController _timelineController;

		[Inject]
		public void Init(WorldGenerator generator, TimelineController timelineController) {
			_generator          = generator;
			_timelineController = timelineController;
		}

		void Start() {
			_generator.Generate();
		}

		[ContextMenu(nameof(FirstStart))]
		public void FirstStart() {
			_timelineController.FirstStart();
		}

		[ContextMenu(nameof(RestartClean))]
		public void RestartClean() {
			_timelineController.Restart();
			SceneManager.LoadScene(0);
		}

		[ContextMenu(nameof(RestartReplay))]
		public void RestartReplay() {
			_timelineController.RestartWithReplay();
			SceneManager.LoadScene(0);
		}

		[ContextMenu(nameof(TravelBackward))]
		public void TravelBackward() {
			_timelineController.TravelBackward();
			SceneManager.LoadScene(0);
		}
	}
}