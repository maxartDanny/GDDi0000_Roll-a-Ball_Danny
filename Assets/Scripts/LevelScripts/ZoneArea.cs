using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class ZoneArea : MonoBehaviour {

	#region Variables

	[SerializeField] private GameObject[] lockWalls;

	private bool clear = false;


	private List<EnemyController> enemies = new List<EnemyController>();

	public bool ZoneActive { get; private set; } = false;

	public bool Clear {
		get { return clear; }
		private set {
			if (clear.Equals(value)) return;
			clear = value;
			ClearUpdateEvent.Invoke(clear);
		}
	}

	public ReturnEvent<bool> ClearUpdateEvent = new ReturnEvent<bool>();


	public List<EnemyController> Enemies => enemies;

	#endregion ^ Variables


	#region Unity Methods

	private void OnTriggerEnter(Collider other) {
		if (Clear) return;

		if (other.CompareTag("Player")) {
			if (other.GetComponent<PlayerController>().IsDead) return;

			ActivateArea();
		} else if (other.CompareTag("Enemy")) {
			enemies.Add(other.GetComponent<EnemyController>());
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Enemy")) {
			enemies.Remove(other.GetComponent<EnemyController>());
		}
	}

	private void Update() {
		if (!ZoneActive) return;

		int length = enemies.Count;
		bool allDead = true;

		if (enemies.Count == 0) {
			ZoneComplete();
		}

		for (int i = 0; i < length; i++) {
			if (enemies[i] != null && !enemies[i].IsPickup) {
				allDead = false;
				break;
			}
		}

		if (allDead) ZoneComplete();
	}

	private void OnDestroy() {
		GameManager.Instance?.PlayerDeathEvent.RemoveListener(OnPlayerDeathEvent);
	}

	#endregion ^ Unity Methods


	#region Event Methods

	private void OnPlayerDeathEvent() {
		ZoneActive = false;
		EnableWalls(false);
	}

	#endregion ^ Event Methods


	#region Helper Methods

	private void ActivateArea() {
		EnableWalls(true);

		ZoneActive = true;
		Clear = false;

		GameManager.Instance.PlayerDeathEvent.AddListener(OnPlayerDeathEvent);

		GameManager.Instance.ZoneActivated(this);
	}

	private void EnableWalls(bool state) {
		int length = lockWalls.Length;

		for (int i = 0; i < length; i++) {
			lockWalls[i].SetActive(state);
		}
	}

	private void ZoneComplete() {
		Clear = true;
		ZoneActive = false;
		EnableWalls(false);

		CheckpointKeeper.SetCheckpoint(transform);

		GameManager.Instance.PlayerDeathEvent.RemoveListener(OnPlayerDeathEvent);
	}

	#endregion ^ Helper Methods
}