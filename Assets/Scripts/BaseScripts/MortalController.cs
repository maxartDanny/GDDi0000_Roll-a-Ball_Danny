using UnityEngine;

/// <summary>
///
/// </summary>
public abstract class MortalController : MonoBehaviour, IDamageable {

	private int health;

	[SerializeField] private IDDamage damageType;


	[SerializeField] private Rigidbody rbody;

	protected int Health {
		get { return health; }
		set {
			if (health.Equals(value)) return;
			health = value;
			HealthUpdateEvent.Invoke(health);
		}
	}

	public IDDamage GetDamageID() => damageType;

	public Rigidbody RBody => rbody;


	public ReturnEvent<int> HealthUpdateEvent = new ReturnEvent<int>();


	public abstract void DamageRecieve(IDDamage damageType, Vector3 sourcePos, Vector3 velocity);
}