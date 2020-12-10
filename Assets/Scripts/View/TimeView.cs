using System;
using TimeLab.Shared;
using TimeLab.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace TimeLab.View {
	[RequireComponent(typeof(TMP_Text))]
	public sealed class TimeView : DisposableOwner {
		TMP_Text _text;

		int _lastValue = -1;

		[Inject]
		public void Init(World world) {
			_text = GetComponent<TMP_Text>();
			SetupDisposables();
			world.Time.Current
				.Subscribe(OnTimeChanged)
				.AddTo(Disposables);
		}

		void OnTimeChanged(double value) {
			var intValue = (int) value;
			if ( intValue == _lastValue ) {
				return;
			}
			_lastValue = intValue;
			_text.text = TimeSpan.FromSeconds(intValue).ToString(@"mm\:ss");
		}
	}
}