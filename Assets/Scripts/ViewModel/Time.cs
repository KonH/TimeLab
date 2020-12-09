using UniRx;

namespace TimeLab.ViewModel {
	public sealed class Time {
		public ReactiveProperty<double> Current { get; } = new ReactiveProperty<double>();
	}
}