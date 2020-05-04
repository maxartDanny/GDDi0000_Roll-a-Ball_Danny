using UnityEngine;

/// <summary>
///
/// </summary>
public class Projectile : MonoBehaviour {

	#region Variables

	[SerializeField] private LayerMask mask;

	public IDDamage DamageType { get; private set; }

	private Vector3 targetPos;
	private Vector3 initialPos;

	public float Speed { get; private set; }

	private const float deathTime = 3.5f;

	private float time = 0;
	private float timeToDestination = 0;

	private Transform myTransform;

	#endregion ^ Variables


	#region Unity Methods

	private void Awake() {
		myTransform = transform;
	}

	private void FixedUpdate() {

		time += Time.fixedDeltaTime;

		myTransform.position = Vector3.LerpUnclamped(initialPos, targetPos, time / timeToDestination);

		if (time > deathTime) Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision collision) {

		if (mask == (mask | (1 << collision.gameObject.layer))) {
			Destroy(gameObject);
		}

	}

	#endregion ^ Unity Methods


	#region Public Methods

	public void Initialize(Transform target, IDDamage damageType, float speed) {
		initialPos = myTransform.position;
		DamageType = damageType;
		Speed = speed;

		targetPos = target.position;

		timeToDestination = (targetPos - initialPos).magnitude / speed;
	}

	public void Deflect(Vector3 direction, IDDamage damageType) {

		DamageType = damageType;

		initialPos = myTransform.position;
		targetPos = initialPos + direction;

		time = 0;
		timeToDestination = direction.magnitude / Speed;

	}

	#endregion ^ Public Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}