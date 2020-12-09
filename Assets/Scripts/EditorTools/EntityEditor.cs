using TimeLab.View;
using UnityEditor;
using UnityEngine;

namespace TimeLab.EditorTools {
	[CustomEditor(typeof(EntityView))]
	public class EntityEditor : Editor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			var view   = (EntityView) serializedObject.targetObject;
			var entity = view.Entity;
			if ( entity == null ) {
				return;
			}
			GUILayout.Label("Components");
			foreach ( var component in entity.Components ) {
				GUILayout.Label($" - {component}");
			}
		}
	}
}