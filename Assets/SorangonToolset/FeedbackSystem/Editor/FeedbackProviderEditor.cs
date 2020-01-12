using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SorangonToolset.FeedbackSystem;

namespace SorangonToolset.FeedbackSystem.EditorCustom {
	[CustomEditor(typeof(FeedbackProvider))]
	public class FeedbackProviderEditor : Editor {
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
		private readonly GUIContent _addEventContent = new GUIContent("Add event", "Add a new event to the list");
		private readonly GUIContent _removeEventContent = new GUIContent("x", "Removes this event from the list");
		private readonly GUIContent _eventNameContent = new GUIContent("Name", "The name of the event you would call");
		private readonly GUIContent _eventFeedbackContent = new GUIContent("Fedback", "The target feedback of the event");
		#endregion

		#region Callbacks
		private void OnEnable() {
			_eventsArray = serializedObject.FindProperty("_feedbackEvents");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();
			UpdateStyles();

			for (int i = 0; i < _eventsArray.arraySize; i++) {
				DrawFeedbackEvent(i);
			}

			GUILayout.Space(6f);
			if (GUILayout.Button(_addEventContent, GUILayout.Width(100f))) {
				_eventsArray.InsertArrayElementAtIndex(_eventsArray.arraySize);
			}

			serializedObject.ApplyModifiedProperties();
		}
		#endregion

		#region Contents
		#endregion

		#region Events
		/// <summary>
		/// Draw an event pannel
		/// </summary>
		/// <param name="eventID"></param>
		private void DrawFeedbackEvent(int eventID) {
			SerializedProperty name = _eventsArray.GetArrayElementAtIndex(eventID).FindPropertyRelative("_name");
			SerializedProperty feedback = _eventsArray.GetArrayElementAtIndex(eventID).FindPropertyRelative("_feedback");

			GUILayout.BeginVertical("HelpBox");

			//Window bar
			GUILayout.BeginHorizontal("HelpBox");
			EditorGUILayout.LabelField(name.stringValue, _eventLabelStyle);
			Color backupColor = GUI.color;
			GUI.color = new Color(0.85f, 0.5f, 0.5f);

			//Delete Button
			if (_eventsArray.arraySize > 1 && GUILayout.Button(_removeEventContent, _deleteButtonStyle, GUILayout.Width(16), GUILayout.Height(16))){
				_eventsArray.DeleteArrayElementAtIndex(eventID);
				return;
			}
			GUI.color = backupColor;
			GUILayout.EndHorizontal();

			EditorGUILayout.PropertyField(name, _eventNameContent);
			EditorGUILayout.PropertyField(feedback, _eventFeedbackContent);

			GUILayout.EndVertical();
		}
		#endregion
	}
}