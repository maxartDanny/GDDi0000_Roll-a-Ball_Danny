using UnityEngine;

/// <summary>
///
/// </summary>
public class GameManager : MonoBehaviour {

	#region Singleton

	private static GameManager instance;

	public static GameManager Instance {
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
			GameObject gameObject = new GameObject("Spawned GameManager");
			instance = gameObject.AddComponent<GameManager>();
			DontDestroyOnLoad(gameObject);

			Debug.LogFormat("Spawned new {0} singleton", typeof(GameManager));
		}
	}

	#endregion ^ Singleton

	#region Properties

	public PlayerController Player { get; private set; }

	public Vector3 PlayerPosition => Player == null ? new Vector3() : Player.transform.position;

	#endregion ^ Properties


	#region Unity Methods



	#endregion ^ Unity Methods


	#region Public Methods

	public void AssignePlayer(PlayerController player) {
		Player = player;
	}

	#endregion ^ Public Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}