using UnityEngine;

/// <summary>
///
/// </summary>
public class HurtBoxController : MonoBehaviour {

	#region Variables

	[SerializeField] private string tagToHit;

	[SerializeField] private MortalController owner;


	[SerializeField] private bool deflect = false;

	#endregion ^ Variables


	#region Unity Methods

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(tagToHit)) {
			IDamageable damageable = other.GetComponent<IDamageable>();
			if (damageable != null) {
				damageable.DamageRecieve(owner.transform, owner.MyDamageID(), owner.transform.position, owner.RBody.velocity);
				GameManager.Instance.HitStop.BigHit();
			}
		} else if (deflect && other.CompareTag("Projectile")) {
			Projectile projectile = other.GetComponent<Projectile>();
			if (projectile.DamageType != owner.MyDamageID() && projectile != null) {
				projectile.Deflect(owner.Direction - projectile.Position, owner.MyDamageID());
				GameManager.Instance.HitStop.SmallHit();
			}
		}
	}

	#endregion ^ Unity Methods

}