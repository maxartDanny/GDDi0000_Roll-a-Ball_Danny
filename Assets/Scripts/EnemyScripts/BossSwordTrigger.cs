using UnityEngine;

/// <summary>
///
/// </summary>
public class BossSwordTrigger : MonoBehaviour {

	#region Variables

	[SerializeField] private BossEnemyController owner;

	#endregion ^ Variables

	#region Unity Methods

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			owner.OnSwordTrigger();
		}
	}

	#endregion ^ Unity Methods


}