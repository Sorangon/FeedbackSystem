using UnityEngine;
using UnityEngine.Events;

public class Mage : MonoBehaviour {
	#region Properties
	[SerializeField] private UnityEvent _onHeal = null;
	[SerializeField] private UnityEvent _onBuffDamages = null;
	[SerializeField] private UnityEvent _onDebuffDamages = null;
	#endregion

	#region Callbacks
	public void OnGUI() {
		if (GUILayout.Button("Heal")) {
			_onHeal.Invoke();
		}

		if (GUILayout.Button("Buff")) {
			_onBuffDamages.Invoke();
		}

		if (GUILayout.Button("Debuff")) {
			_onDebuffDamages.Invoke();
		}
	}
	#endregion
}