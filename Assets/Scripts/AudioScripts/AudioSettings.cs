using UnityEngine;

namespace Audio {

	/// <summary>
	///
	/// </summary>
	[CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/AudioSettings", order = 1)]
	public class AudioSettings : ScriptableObject {

		[SerializeField] private AudioSet[] audioSets;

		public AudioSet GetSet(string name) {
			int length = audioSets.Length;

			if (length == 0) return new AudioSet();

			AudioSet set = new AudioSet();
			for (int i = 0; i < length; i++) {
				set = audioSets[i];
				if (name.Equals(set.Name)) {
					return set;
				}
			}

			return set;
		}

	}
}