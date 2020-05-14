using Audio;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Projectile : MonoBehaviour {

	#region Variables

	private const float HEIGHT = 0.5f;

	[SerializeField] private LayerMask mask;

	[SerializeField] private AnimationCurve arch;

	[SerializeField] private Rigidbody rbody;

	[SerializeField] private AudioSource audioSource;


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

	public Vector3 Position => myTransform.position;

	public float Speed { get; private set; }

	public Vector3 Velocity => rbody.velocity;

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

		//if (IsArching) {

		//	Debug.LogFormat("{0}", CalcParabola(t));
		//	newPos += archDir * CalcParabola(t);
		//}

		myTransform.position = newPos;

		if (time > deathTime) Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision collision) {

		if (mask == (mask | (1 << collision.gameObject.layer))) {
			Destroy(gameObject);
		}

	}

	//private void OnDrawGizmos() {
	//	Gizmos.DrawLine(initialPos, targetPos);
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawLine(initialPos, initialPos + archDir);
	//}

	#endregion ^ Unity Methods


	#region Public Methods

	public void Initialize(Transform target, IDDamage damageType, float speed) {
		initialPos = myTransform.position;
		DamageType = damageType;
		Speed = speed;

		targetPos = target.position;
		targetPos.y = HEIGHT;
		archDir = Vector3.Cross(targetPos - initialPos, Vector3.up).normalized;
		distToTarget = (targetPos - initialPos).magnitude;

		timeToDestination = distToTarget / speed;

		IsArching = true;

		GameManager.Instance.HitStop.SmallHit();
	}

	public void Deflect(Vector3 direction, IDDamage damageType) {
		AudioManager.Instance.PlayAudio(audioSource, Audio.Mortal.DEFLECT_SOUND);

		DamageType = damageType;

		initialPos = myTransform.position;
		initialPos.y = HEIGHT;
		targetPos = initialPos + direction;
		targetPos.y = HEIGHT;

		time = 0;
		Speed *= 1.25f;
		timeToDestination = direction.magnitude / Speed;
		IsArching = false;

		GameManager.Instance.HitStop.BigHit();
	}

	#endregion ^ Public Methods


	#region Helper Methods

	private float CalcParabola(float x) {
		// -(x-1)^2 -x + 1
		return -((x - 1) * (x - 1)) - x + 1;
	}

	#endregion ^ Helper Methods
}