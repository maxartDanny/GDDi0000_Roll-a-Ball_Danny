using UnityEngine;

/// <summary>
///
/// </summary>
public class BasicEnemyController : EnemyController {

	#region Variables

	[SerializeField] private float damage = 10;

	private float maxImpact = 2;

	#endregion ^ Variables


	#region Unity Methods

	#endregion ^ Unity Methods


	#region Implementation Methods

	public override void DamageRecieve(IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {

		Vector3 myPos = transform.position;
		sourcePos.y = myPos.y;
		float impact = Mathf.Clamp(velocity.magnitude, 0, maxImpact);

		RBody?.AddForce(((Vector3.up * 0.5f) + velocity.normalized).normalized * (damage + impact), ForceMode.Impulse);

	}

	#endregion ^ Implementation Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}