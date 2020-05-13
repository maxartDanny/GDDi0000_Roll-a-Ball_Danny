using UnityEngine;
using UnityEngine.Events;

/// <summary>
///
/// </summary>
public class GameManager : MonoBehaviour {

	#region Singleton

	private static GameManager instance;
	private static bool applicationQuitting = false;

	public static GameManager Instance {
		get {
			Init();
			return instance;
		}
		private set { instance = value; }
	}

	[RuntimeInitializeOnLoadMethod]
	private static void Init() {

		if (applicationQuitting) return;

		Application.quitting += OnApplicationExit;

		// Unity replaces a destroyed game object with a non-null "destroyed" object.
		// Using .Equals checks against that.
		if (instance == null || instance.Equals(null)) {
			GameObject gameObject = new GameObject("Spawned GameManager");
			instance = gameObject.AddComponent<GameManager>();
			DontDestroyOnLoad(gameObject);

			Debug.LogFormat("Spawned new {0} singleton", typeof(GameManager));
		}
	}

	private static void OnApplicationExit() {
		Application.quitting -= OnApplicationExit;
		applicationQuitting = true;
	}

	#endregion ^ Singleton


	#region Variables

	private float deathTime = 3f;
	private float deathTimer = 0f;

	private const string COMPLETION_PREF_ID = "Temp";
	private bool gameCompleted = false;

	#endregion ^ Variables

	#region Properties

	public PlayerController Player { get; private set; }

	public Vector3 PlayerPosition => Player == null ? new Vector3() : Player.transform.position;

	public HitStopManager HitStop { get; private set; }

	public bool GameCompleted => gameCompleted;

	#endregion ^ Properties

	#region Events

	public UnityEvent PlayerDeathEvent = new UnityEvent();

	#endregion ^ Events


	#region Unity Methods

	private void Awake() {
		HitStop = gameObject.AddComponent<HitStopManager>();

		if (PlayerPrefs.HasKey(COMPLETION_PREF_ID)) {
			gameCompleted = PlayerPrefs.GetInt(COMPLETION_PREF_ID) > 0;
		}

		Debug.LogFormat("completion: {0}", gameCompleted);
	}

	private void OnDestroy() {
		Player?.HealthUpdateEvent.RemoveListener(OnPlayerHealthUpdate);
	}

	private void Update() {
		if (deathTimer > 0) {
			deathTimer -= Time.deltaTime;

			if (deathTimer <= 0)
				Player?.Respawn(CheckpointKeeper.LastCheckpoint);
		}
	}

	#endregion ^ Unity Methods


	#region Public Methods

	public void AssignPlayer(PlayerController player) {
		Player = player;
		player.HealthUpdateEvent.AddListener(OnPlayerHealthUpdate);
	}

	public void BossComplete() {
		gameCompleted = true;
		PlayerPrefs.SetInt(COMPLETION_PREF_ID, 1);
		PlayerPrefs.Save();
	}

	#endregion ^ Public Methods


	#region Event Methods

	private void OnPlayerHealthUpdate(int playerHealth) {
		if (playerHealth <= 0) {
			// player dead
			deathTimer = deathTime;
			PlayerDeathEvent?.Invoke();
		}
	}

	#endregion ^ Event Methods
}