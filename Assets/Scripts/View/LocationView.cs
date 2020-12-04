using TimeLab.ViewModel;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	public sealed class LocationView : MonoBehaviour {
		[SerializeField] Transform _placeholder;

		[Inject]
		public void Init(Location location) {
			gameObject.name         = $"Location_{location.Id}";
			transform.position      = location.Bounds.Center;
			_placeholder.localScale = new Vector3(location.Bounds.Size.x * 10, location.Bounds.Size.y * 10, 1);
		}
	}
}