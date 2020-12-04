using System;
using UniRx;
using UnityEngine;

namespace TimeLab.Shared {
	/// <summary>
	/// Helper to simplify UniRx initialization
	/// </summary>
	public abstract class DisposableOwner : MonoBehaviour, IDisposable {
		protected CompositeDisposable Disposables { get; private set; }

		protected void SetupDisposables() {
			Dispose();
			Disposables = new CompositeDisposable();
		}

		public void Dispose() => Disposables?.Dispose();

		protected void OnDestroy() => Dispose();
	}
}