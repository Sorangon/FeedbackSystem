using System.Collections;
using UnityEngine;

namespace SorangonToolset.FeedbackSystem {
	/// <summary>
	/// This component simply destroy the object after a delay when it's triggered from a feedback audio source
	/// </summary>
	internal class FeedbackAudioSource : MonoBehaviour {
		#region Components
		private AudioSource _source = null;
		#endregion

		#region Properties
		internal AudioSource Source => _source;
		#endregion

		#region Initialize
		/// <summary>
		/// Create a audio source on the game object
		/// </summary>
		internal void Init() {
			_source = gameObject.AddComponent<AudioSource>();
			_source.playOnAwake = false;
			_source.loop = false;
		}

		/// <summary>
		/// Get target audio source
		/// </summary>
		internal void GetSource() {
			_source = GetComponent<AudioSource>();
		}
		#endregion

		#region Play
		/// <summary>
		/// Launch auto destruction timer for set value
		/// </summary>
		/// <param name="time"></param>
		internal void Play(float time) {
			_source.Play();
			StartCoroutine(DestroyTimer(time));
		}
		#endregion

		#region Coroutines
		private IEnumerator DestroyTimer(float time) {
			for(float f = time; f >= 0f; f -= Time.deltaTime) {
				yield return null;
			}
			Destroy(gameObject);
			yield return null;
		}
		#endregion
	}
}

