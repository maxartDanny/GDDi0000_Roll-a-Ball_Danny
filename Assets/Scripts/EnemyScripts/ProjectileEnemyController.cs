using System.Collections;
using UnityEngine;

/// <summary>
///
/// </summary>
public class ProjectileEnemyController : BasicEnemyController {

	#region Variables

	[SerializeField] private float projectileSpeed = 40;

	[SerializeField] private GameObject projectilePrefab;

	#endregion ^ Variables

	#region Public Methods

	public override void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {
		base.DamageRecieve(other, damageType, sourcePos, velocity);

		actionStack.Push(() => Attack(other));
	}

	#endregion ^ Public Methods


	#region Helper Methods

	private void Attack(Transform target) {
		StartCoroutine(AttackSequence(target, 0.8f));
	}

	private IEnumerator AttackSequence(Transform target, float waitTime) {
		yield return new WaitForSeconds(waitTime);

		Vector3 dir = (target.position - transform.position).normalized * transform.localScale.x;
		Quaternion look = Quaternion.LookRotation(dir);

		GameObject projectile = Instantiate(projectilePrefab, transform.position + dir, look) as GameObject;

		projectile.GetComponent<Projectile>().Initialize(target, GetDamageID(), projectileSpeed);

		ActionComplete();

	}

	#endregion ^ Helper Methods
}