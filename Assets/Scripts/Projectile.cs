using UnityEngine;

/// <summary>
///
/// </summary>
public class Projectile : MonoBehaviour {

	#region Variables

	[SerializeField] private LayerMask mask;

	[SerializeField] private AnimationCurve arch;


	private Vector3 targetPos;
	private Vector3 initialPos;
	private Vector3 archDir;

	private const float deathTime = 3.5f;

	private float time = 0;
	private float distToTarget = 0;
	private float timeToDestination = 0;

	private Transform myTransform;

	#endregion ^ Variables


	#region Properties

	public IDDamage DamageType { get; private set; }

	public float Speed { get; private set; }

	private bool IsArching { get; set; }

	#endregion ^ Properties


	#region Unity Methods

	private void Awake() {
		myTransform = transform;
	}

	private void FixedUpdate() {

		time += Time.fixedDeltaTime;

		float t = time / timeToDestination;
		Vector3 newPos = Vector3.LerpUnclamped(initialPos, targetPos, t);

		if (IsArching) {

			Debug.LogFormat("{0}", CalcParabola(t));
			newPos += archDir * CalcParabola(t);
		}

		myTransform.position = newPos;

		if (time > deathTime) Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision collision) {

		if (mask == (mask | (1 << collision.gameObject.layer))) {
			Destroy(gameObject);
		}

	}

	private void OnDrawGizmos() {
		Gizmos.DrawLine(initialPos, targetPos);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(initialPos, initialPos + archDir);
	}

	#endregion ^ Unity Methods


	#region Public Methods

	public void Initialize(Transform target, IDDamage damageType, float speed) {
		initialPos = myTransform.position;
		DamageType = damageType;
		Speed = speed;

		targetPos = target.position;
		archDir = Vector3.Cross(targetPos - initialPos, Vector3.up).normalized;
		distToTarget = (targetPos - initialPos).magnitude;

		timeToDestination = distToTarget / speed;

		IsArching = true;
	}

	public void Deflect(Vector3 direction, IDDamage damageType) {

		DamageType = damageType;

		initialPos = myTransform.position;
		targetPos = initialPos + direction;

		time = 0;
		timeToDestination = direction.magnitude / Speed;
		IsArching = false;
	}

	#endregion ^ Public Methods


	#region Helper Methods

	private float CalcParabola(float x) {
		// -(x-1)^2 -x + 1
		return -((x - 1) * (x - 1)) - x + 1;
	}

	#endregion ^ Helper Methods
}