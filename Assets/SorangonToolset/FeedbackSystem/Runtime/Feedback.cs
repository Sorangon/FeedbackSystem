using UnityEngine;

namespace SorangonToolset.FeedbackSystem {
	/// <summary>
	/// Feedback class manages a one shot feedback that contains a visual and a sound effect
	/// </summary>
	[CreateAssetMenu(menuName = "Feedback", fileName = "NewFeedback", order = 800)]
	public class Feedback : ScriptableObject {
		#region Properties
		[Header("Visual Effect")]
		[SerializeField, Tooltip("The target particle system to instantiate")]
		private ParticleSystem _particleSystem = null;

		[SerializeField, Min(0.0f), Tooltip("The multiplication value of the visual effect sacle")]
		private float _scaleMultiplier = 1.0f;

		[SerializeField, Tooltip("Does initial rotation is relative to the owner's one")]
		private bool _followOwnerRotation = false;

		[Header("Sound Effect")]
		[SerializeField, Tooltip("The target sound effect to instantiate")]
		private AudioClip _soundEffect = null;

		[SerializeField, Range(0f, 1f), Tooltip("The value of the sound effect volume")]
		private float _volume = 1.0f;

		[SerializeField, Tooltip("The random range of the sound effect pitch")]
		private Vector2 _randomPitchRange = new Vector2(1.0f, 1.0f);

		[SerializeField, Range(0f, 1f), Tooltip("For 0, the sound os entirely 2D, for 1 the sound is 3D")]
		private float _spatialBlend = 0.5f;

		[Header("General Settings")]
		[SerializeField, Tooltip("Does the instance will be parented to the owner")]
		private bool _parentToOwner = false;
		#endregion

		#region Accessors
		private AudioSource _source {
			get {
				if (_sourceInstance == null) {
					_sourceInstance = InitAudioSource();
				}

				return _sourceInstance;
			}
		}
		#endregion

		#region Current
		private static AudioSource _sourceInstance;
		#endregion

		#region Feedback Execution



		/// <summary>
		/// Plays a feedback at a position and a rotation in world space
		/// </summary>
		/// <param name="position">The target feedback position</param>
		/// <param name="rotation">The target feedback rotation</param>
		/// <param name="owner">The target owner of the feedback</param>
		public void Play(Vector3 position, Quaternion rotation, Transform owner = null) {
			Transform parent = null;
			if (owner != null && _parentToOwner) {
				parent = owner;
			}

			//Instantiates a particle system
			if (_particleSystem != null) {
				if (_followOwnerRotation && owner != null) rotation *= owner.rotation;
				ParticleSystem vfxInstance = Instantiate(_particleSystem, position, rotation, parent);
				vfxInstance.transform.localScale *= _scaleMultiplier;
			}

			//Instantiates a one shot audio source
			if (_soundEffect != null) {
				AudioSource sfxInstance = Instantiate(_source, position, Quaternion.identity, parent);
				sfxInstance.clip = _soundEffect;
				sfxInstance.pitch = Random.Range(_randomPitchRange.x, _randomPitchRange.y);
				sfxInstance.volume = _volume;
				sfxInstance.spatialBlend = _spatialBlend;
				sfxInstance.Play();

				//Sends a message to the AutoDestroyer to lauch the timer
				sfxInstance.BroadcastMessage("Launch", _soundEffect.length);
			}
		}

		/// <summary>
		/// Plays the feedback at a position in world space
		/// </summary>
		/// <param name="position">The target feedback position</param>
		/// <param name="owner">The target owner of the feedback</param>
		public void Play(Vector3 position, Transform owner = null) {
			Play(position, Quaternion.identity, owner);
		}

		#endregion

		#region Audio Source
		private AudioSource InitAudioSource() {
			GameObject sourceGO = new GameObject("One Shot Audio Source");
			AudioSource source = sourceGO.AddComponent<AudioSource>();
			sourceGO.gameObject.AddComponent<FeedbackAutoDestroyer>();
			source.playOnAwake = false;
			source.loop = false;
			DontDestroyOnLoad(sourceGO);
			return source;
		}
		#endregion
	}

}