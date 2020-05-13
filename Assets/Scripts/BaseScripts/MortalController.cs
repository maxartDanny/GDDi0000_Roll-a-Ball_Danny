using UnityEngine;

/// <summary>
///
/// </summary>
public abstract class MortalController : MonoBehaviour, IDamageable {

	[SerializeField] private int health = 1;

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

	public bool IsDead => health <= 0;

	public IDDamage MyDamageID() => damageType;

	public Rigidbody RBody => rbody;

	public virtual Vector3 Direction => transform.forward;


	public ReturnEvent<int> HealthUpdateEvent = new ReturnEvent<int>();

	public void AddHealth(int amount) {
		Health += amount;
	}

	public abstract void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity);
}