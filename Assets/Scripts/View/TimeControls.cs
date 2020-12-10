using TimeLab.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace TimeLab.View {
	public sealed class TimeControls : MonoBehaviour {
		[SerializeField] Button _backButton;
		[SerializeField] Button _forwardButton;

		TimelineController _timelineController;

		[Inject]
		public void Init(TimelineController timelineController) {
			_timelineController = timelineController;
			_backButton.onClick.AddListener(TravelBackward);
			_forwardButton.onClick.AddListener(TravelForward);
		}

		void TravelBackward() {
			_timelineController.Travel(-30);
			SceneManager.LoadScene(0);
		}

		void TravelForward() {
			_timelineController.Travel(30);
			SceneManager.LoadScene(0);
		}

	}
}