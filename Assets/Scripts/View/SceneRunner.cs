using TimeLab.Manager;
using TimeLab.Shared;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TimeLab.View {
	public sealed class SceneRunner : MonoBehaviour {
		TimeProvider       _timeProvider;
		WorldGenerator     _generator;
		TimelineController _timelineController;

		[Inject]
		public void Init(TimeProvider timeProvider, WorldGenerator generator, TimelineController timelineController) {
			_timeProvider       = timeProvider;
			_generator          = generator;
			_timelineController = timelineController;
		}

		void Start() {
			if ( _timeProvider.IsRealTime ) {
				_generator.Generate();
			}
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