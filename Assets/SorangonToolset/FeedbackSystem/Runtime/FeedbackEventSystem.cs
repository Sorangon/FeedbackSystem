using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorangonToolset.FeedbackSystem {
	/// <summary>
	/// Contains multiple feedbacks event that can be called
	/// </summary>
	[AddComponentMenu("Sorangon Toolset/Feedback System/Feedback Event System")]
	public class FeedbackEventSystem : MonoBehaviour {
		#region SubClasses
		[System.Serializable]
		public class FeedbackEvent {
			[SerializeField] private string _name = "Feedback";
			[SerializeField] private Feedback _feedback = null;

			public string Name => _name;
			public Feedback Feedback => _feedback;

			public FeedbackEvent() {
				_name = "Feedback";
				_feedback = null;
			}
		}
		#endregion

		#region Callbacks
#if UNITY_EDITOR
		private void Reset() {
			//Creates at least one feedback event when the component is created
			_feedbackEvents = new FeedbackEvent[1] { new FeedbackEvent() };
		}
#endif
		#endregion

		#region Properties
		[SerializeField] private FeedbackEvent[] _feedbackEvents = { };
		#endregion

		#region Manage Feedbacks
		/// <summary>
		/// Executes the target feedback event
		/// </summary>
		/// <param name="eventName">The name of the event</param>
		public void PlayFeedbackEvent(string eventName) {
			for (int i = 0; i < _feedbackEvents.Length; i++) {
				if (_feedbackEvents[i].Name == eventName) {
					if(_feedbackEvents[i] != null) {
						_feedbackEvents[i].Feedback.Play(transform.position, transform);

					}
					else {
						Debug.LogError(eventName + " doesn't contains a target feedback, ensure that one is corectly referenced before call it");
					}
					return;
				}
			}

			Debug.LogWarning("There no feedback with the name " + eventName + ". Ensure that the event name is correct");
		}
		#endregion
	}
}

