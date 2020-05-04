using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class ScoreView : MonoBehaviour {

	#region Variables

	[SerializeField] private Text scoreText;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		ScoreManager.Instance.ScoreCountUpdateEvent.AddListener(OnScoreUpdate);
		OnScoreUpdate(ScoreManager.Instance.ScoreCount);
	}

	#endregion ^ Unity Methods


	#region Event Methods

	private void OnScoreUpdate(int score) {
		scoreText.text = $"Score: {score}";
	}

	#endregion ^ Event Methods

}