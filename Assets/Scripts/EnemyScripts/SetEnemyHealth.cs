using UnityEngine;

public class SetEnemyHealth : MonoBehaviour {

	[SerializeField] private int health = 1;

	private void Awake() {
		if (GameManager.Instance.GameCompleted)
			GetComponent<EnemyController>().AddHealth(health);
	}
}