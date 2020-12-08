using TimeLab.View;
using UnityEditor;
using UnityEngine;

namespace TimeLab.EditorTools {
	[CustomEditor(typeof(EntityView))]
	public class EntityEditor : Editor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			var view = (EntityView) serializedObject.targetObject;
			GUILayout.Label("Components");
			foreach ( var component in view.Entity.Components ) {
				GUILayout.Label($" - {component}");
			}
		}
	}
}