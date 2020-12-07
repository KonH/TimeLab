using TimeLab.Shared;
using TimeLab.ViewModel;
using UniRx;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	/// <summary>
	/// Controls common entity state presentation
	/// </summary>
	public sealed class EntityView : DisposableOwner {
		public sealed class Pool : MonoMemoryPool<Transform, Entity, EntityView> {
			protected override void Reinitialize(Transform parent, Entity entity, EntityView item) {
				item.transform.parent = parent;
				item.Init(entity);
			}
		}

		[SerializeField] EntityRenderHolder _renderHolder;

		void Init(Entity entity) {
			SetupDisposables();
			gameObject.name = $"Entity_{entity.Id}";
			entity.Position
				.Subscribe(OnPositionChange)
				.AddTo(Disposables);
			_renderHolder.Init(entity);
		}

		void OnPositionChange(Vector2Int position) {
			transform.localPosition = new Vector3(position.x, position.y);
		}
	}
}