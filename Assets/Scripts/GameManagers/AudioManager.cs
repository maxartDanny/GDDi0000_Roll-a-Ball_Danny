using UnityEngine;
using UnityEngine.Assertions;

namespace Audio {

	/// <summary>
	///
	/// </summary>
	public class AudioManager : MonoBehaviour {

		#region Singleton

		private static AudioManager instance;

		public static AudioManager Instance {
			get {
				Init();
				return instance;
			}
			private set { instance = value; }
		}

		[RuntimeInitializeOnLoadMethod]
		private static void Init() {
			// Unity replaces a destroyed game object with a non-null "destroyed" object.
			// Using .Equals checks against that.
			if (instance == null || instance.Equals(null)) {
				GameObject gameObject = new GameObject("Spawned AudioManager");
				instance = gameObject.AddComponent<AudioManager>();
				DontDestroyOnLoad(gameObject);

				Debug.LogFormat("Spawned new {0} singleton", typeof(AudioManager));
			}
		}

		#endregion ^ Singleton


		#region Variables

		private AudioSettings settings;

		#endregion ^ Variables


		#region Unity Methods

		private void Awake() {
			settings = Resources.Load<AudioSettings>("AudioSettings");

			Assert.IsNotNull(settings, "Unable to find AudioSettings from Resources folder");
		}

		#endregion ^ Unity Methods


		#region Public Methods

		public bool PlayAudio(AudioSource source, string name) {
			if (settings == null) return false;

			AudioSet set = settings.GetSet(name);
			if (string.IsNullOrEmpty(set.Name)) {
				Debug.LogFormat("Unable to find sound of: {0}", name);
				return false;
			}

			source.Stop();
			source.clip = set.GetClip();
			source.pitch = set.GetPitch;
			source.Play();

			return true;
		}

		public bool PlayAudio(string name, Vector3 position) {
			if (settings == null) return false;

			AudioSet set = settings.GetSet(name);
			if (string.IsNullOrEmpty(set.Name)) {
				Debug.LogFormat("Unable to find sound of: {0}", name);
				return false;
			}

			AudioSource.PlayClipAtPoint(set.GetClip(), position);

			return true;
		}

		#endregion ^ Public Methods


		#region Helper Methods



		#endregion ^ Helper Methods
	}
}