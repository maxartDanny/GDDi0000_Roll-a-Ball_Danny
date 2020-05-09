using UnityEngine;

namespace Audio {

	[System.Serializable]
	public struct AudioSet {
		public string Name;
		public AudioClip[] clips;
		public Vector2 PitchRange;
		public float GetPitch => Random.Range(PitchRange.x, PitchRange.y);

		public AudioClip GetClip() {
			int length = clips.Length;
			if (length == 0) return null;
			if (length == 1) return clips[0];

			return clips[Random.Range(0, length)];
		}
	}
}