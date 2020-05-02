using UnityEngine;

/// <summary>
///
/// </summary>
public class BasicEnemyController : EnemyController {

	#region Variables

	[SerializeField] private float damage = 10;

	#endregion ^ Variables


	#region Unity Methods

	#endregion ^ Unity Methods


	#region Implementation Methods

	public override void DamageRecieve(IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {

		Vector3 myPos = transform.position;
		sourcePos.y = myPos.y;
		float impact = velocity.magnitude;

		RBody?.AddForce(((Vector3.up * impact * 0.5f) + velocity).normalized * (damage + impact), ForceMode.Impulse);

	}

	#endregion ^ Implementation Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}