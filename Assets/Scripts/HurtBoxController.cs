using UnityEngine;

/// <summary>
///
/// </summary>
public class HurtBoxController : MonoBehaviour {

	#region Variables

	[SerializeField] private string tagToHit;


	[SerializeField] private MortalController owner;

	#endregion ^ Variables


	#region Unity Methods

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(tagToHit)) {
			IDamageable damageable = other.GetComponent<IDamageable>();
			if (damageable != null) {
				damageable.DamageRecieve(owner.transform, owner.GetDamageID(), owner.transform.position, owner.RBody.velocity);
				GameManager.Instance.HitStop.BigHit();
			}
		}
	}

	#endregion ^ Unity Methods

}