using System.Linq;
using TimeLab.Component;
using TimeLab.Shared;
using TimeLab.ViewModel;
using UniRx;
using Zenject;

namespace TimeLab.View {
	public sealed class EntityRenderHolder : DisposableOwner {
		EntityRenderView.PooledFactory _factory;

		Entity _entity;

		string           _currentType;
		EntityRenderView _currentView;

		[Inject]
		public void PreInit(EntityRenderView.PooledFactory factory) {
			_factory = factory;
		}

		public void Init(Entity entity) {
			SetupDisposables();
			var renderComponents = entity.Components
				.OfType<RenderComponent>();
			ChangeRender(renderComponents.FirstOrDefault());
			entity.Components
				.ObserveAdd()
				.Select(ev => ev.Value is RenderComponent render ? render : null)
				.Where(r => (r != null))
				.Subscribe(OnComponentAdd)
				.AddTo(Disposables);
			entity.Components
				.ObserveRemove()
				.Select(ev => ev.Value is RenderComponent render ? render : null)
				.Where(r => (r != null))
				.Subscribe(OnComponentRemove)
				.AddTo(Disposables);
		}

		void OnComponentAdd(RenderComponent render) => ChangeRender(render);

		void OnComponentRemove(RenderComponent _) => ChangeRender(null);

		void ChangeRender(RenderComponent render) {
			var isPresent = (render != null);
			if ( !isPresent ) {
				if ( string.IsNullOrEmpty(_currentType) ) {
					return;
				}
				_factory.Despawn(_currentType, _currentView);
				_currentType = string.Empty;
				return;
			}
			_currentView = _factory.Spawn(render.Type);
			if ( !_currentView ) {
				return;
			}
			_currentType = render.Type;
			_currentView.transform.SetParent(transform, false);
		}
	}
}