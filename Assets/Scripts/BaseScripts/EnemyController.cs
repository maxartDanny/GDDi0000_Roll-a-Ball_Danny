
using UnityEngine;
/// <summary>
///
/// </summary>
public abstract class EnemyController : MortalController {

	public bool IsPickup { get; protected set; } = false;

	#region Implementation Methods

	public override void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity) { }

	#endregion ^ Implementation Methods

}