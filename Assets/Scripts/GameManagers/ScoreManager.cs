using UnityEngine;

/// <summary>
///
/// </summary>
public class ScoreManager : MonoBehaviour {

	#region Singleton

	private static ScoreManager instance;

	public static ScoreManager Instance {
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
			GameObject gameObject = new GameObject("Spawned ScoreManager");
			instance = gameObject.AddComponent<ScoreManager>();
			DontDestroyOnLoad(gameObject);

			Debug.LogFormat("Spawned new {0} singleton", typeof(ScoreManager));
		}
	}

	#endregion ^ Singleton

	private int scoreCount = 0;

	public int ScoreCount {
		get { return scoreCount; }
		private set {
			if (scoreCount.Equals(value)) return;
			scoreCount = value;
			ScoreCountUpdateEvent.Invoke(scoreCount);
		}
	}

	public ReturnEvent<int> ScoreCountUpdateEvent = new ReturnEvent<int>();

	#region Unity Methods

	private void OnDestroy() {
		ScoreCountUpdateEvent?.RemoveAllListeners();
	}

	#endregion ^ Unity Methods

	public void AddScore(int points) {
		ScoreCount += points;
	}
}