using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class ZoneArea : MonoBehaviour {

	#region Variables


	[SerializeField] private GameObject[] lockWalls;

	private List<EnemyController> enemies = new List<EnemyController>();

	private bool ZoneActive { get; set; } = false;

	private bool Clear { get; set; } = false;

	#endregion ^ Variables


	#region Unity Methods

	private void OnTriggerEnter(Collider other) {
		if (Clear) return;

		if (other.CompareTag("Player")) {
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

	#endregion ^ Unity Methods


	#region Public Methods



	#endregion ^ Public Methods


	#region Helper Methods

	private void ActivateArea() {
		EnableWalls(true);

		ZoneActive = true;
		Clear = false;
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
	}

	#endregion ^ Helper Methods
}