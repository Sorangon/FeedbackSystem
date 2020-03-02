using UnityEngine;
using UnityEditor;

namespace SorangonToolset.FeedbackSystem.EditorCustom {
	[CustomEditor(typeof(FeedbackEventSystem))]
	public class FeedbackEventSystemInspector : Editor {
		#region Serialized Properties
		private SerializedProperty _eventsArray = null;
		#endregion

		#region Styles
		private GUIStyle _deleteButtonStyle;
		private GUIStyle _eventLabelStyle;

		private void UpdateStyles() {
			_deleteButtonStyle = new GUIStyle(GUI.skin.GetStyle("Button")) {
				alignment = TextAnchor.MiddleCenter,
				fontStyle = FontStyle.Bold
			};

			_eventLabelStyle = new GUIStyle(GUI.skin.GetStyle("Label")) {
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.MiddleLeft
			};
		}
		#endregion

		#region Contents
		private readonly GUIContent _addEventContent = new GUIContent("Add event", "Add a new event");
		private readonly GUIContent _removeEventContent = new GUIContent("x", "Remove this event");
		private readonly GUIContent _emptyContent = new GUIContent("");
		#endregion

		#region Callbacks
		private void OnEnable() {
			_eventsArray = serializedObject.FindProperty("_feedbackEvents");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();
			UpdateStyles();
			GUILayout.Space(12f);

			//Events Pannel
			GUILayout.BeginVertical("Button");
			for (int i = 0; i < _eventsArray.arraySize; i++) {
				DrawFeedbackEvent(i);
			}
			GUILayout.EndVertical();

			//Add Event button
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("");//Empty label to align the button on the right
			if (GUILayout.Button(_addEventContent, GUILayout.Width(100f))) {
				_eventsArray.InsertArrayElementAtIndex(_eventsArray.arraySize);
			}
			GUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();
		}
		#endregion

		#region Events
		/// <summary>
		/// Draw an event pannel
		/// </summary>
		/// <param name="eventID"></param>
		private void DrawFeedbackEvent(int eventID) {
			SerializedProperty name = _eventsArray.GetArrayElementAtIndex(eventID).FindPropertyRelative("_name");
			SerializedProperty feedback = _eventsArray.GetArrayElementAtIndex(eventID).FindPropertyRelative("_feedback");

			GUILayout.BeginHorizontal("HelpBox");
			EditorGUILayout.LabelField(" Event", GUILayout.Width(55f));
			EditorGUILayout.PropertyField(name, _emptyContent, GUILayout.Width(90f));
			GUILayout.Space(5f);
			EditorGUILayout.PropertyField(feedback, _emptyContent);
			GUILayout.Space(15f);

			//Delete Button
			Color backupColor = GUI.color;
			GUI.color = new Color(0.85f, 0.5f, 0.5f);
			if (_eventsArray.arraySize > 1 && GUILayout.Button(_removeEventContent, _deleteButtonStyle, GUILayout.Width(18), GUILayout.Height(17))){
				_eventsArray.DeleteArrayElementAtIndex(eventID);
				return;
			}
			GUI.color = backupColor;

			GUILayout.EndHorizontal();
		}
		#endregion
	}
}