using UnityEngine;

namespace Audio {

	/// <summary>
	///
	/// </summary>
	public class PlayAudio : MonoBehaviour {

		#region Variables

		[SerializeField] private string audioName;

		[SerializeField] private AudioSource audioSource;

		#endregion ^ Variables


		#region Public Methods

		public void PlayClip() {
			AudioManager.Instance.PlayAudio(audioSource, audioName);
		}

		#endregion ^ Public Methods

	}
}