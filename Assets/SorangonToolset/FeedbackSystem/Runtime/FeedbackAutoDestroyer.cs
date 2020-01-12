using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorangonToolset.FeedbackSystem {
	/// <summary>
	/// This component simply destroy the object after a delay when it's triggered from a feedback audio source
	/// </summary>
	public class FeedbackAutoDestroyer : MonoBehaviour {

		#region Auto Destruction
		/// <summary>
		/// Launch autodestruction timer for set value
		/// </summary>
		/// <param name="time"></param>
		public void Launch(float time) {
			Invoke("DestroySelf", time);
		}

		private void DestroySelf() {
			Destroy(gameObject);
		}
		#endregion
	}
}

