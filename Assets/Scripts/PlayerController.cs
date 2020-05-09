using UnityEngine;
using Audio;

/// <summary>
///
/// </summary>
public class PlayerController : MortalController {

	#region Variables

	[SerializeField] private float speed = 1;

	[SerializeField] private PlayerLegActionController legActionController;

	[SerializeField] private PlayerInputHandler inputs;

	[SerializeField] private Transform mouseDirection;


	[SerializeField] private AudioSource audioSource;

	private Transform myTransform;
	private Vector3 prevPos = new Vector3();
	private Vector3 calcVelocity = new Vector3();

	private float moveDamper = 0.3f;


	private float dashPower = 2f;
	private float dashTimer = 0f;
	private float dashCooldown = 0.7f;

	#endregion ^ Variables


	#region Properties

	public float DashTimer => dashTimer;
	public float DashCooldown => dashCooldown;

	#endregion ^ Properties


	#region Events

	[SerializeField] private FloatEvent DashActivatedEvent = new FloatEvent();

	#endregion ^ Events


	#region Unity Methods

	private void Awake() {
		GameManager.Instance.AssignePlayer(this);
	}

	private void Start() {
		RBody.maxAngularVelocity = float.MaxValue;
		myTransform = transform;
		prevPos = myTransform.position;
	}

	private void FixedUpdate() {

		if (dashTimer > 0) {
			dashTimer -= Time.fixedDeltaTime;

			if (dashTimer <= 0) {
			}
		}

		calcVelocity = (myTransform.position - prevPos) / Time.fixedDeltaTime;
		prevPos = myTransform.position;

		Vector3 movement = new Vector3(inputs.Horizontal, 0, inputs.Vertical).normalized * speed * Time.fixedDeltaTime;

		Vector3 velocity = RBody.velocity;

		//speedVector += movement;
		//speedVector.y = RBody.velocity.y;

		//Debug.LogFormat("Speed Vector: {0}", speedVector.magnitude);

		//RBody.velocity *= Mathf.LerpUnclamped(0.95f, 0, Time.fixedDeltaTime);
		RBody.AddForce(-velocity * moveDamper);
		RBody.velocity += movement;

	}

	private void OnDestroy() {
		DashActivatedEvent?.RemoveAllListeners();
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Enemy")) {

			Debug.LogFormat("Hit enemy RB: {0} | calc: {1}", RBody.velocity.magnitude, calcVelocity.magnitude);

			if (calcVelocity.magnitude >= 8f) {

				IDamageable damageable = collision.transform.GetComponent<IDamageable>();

				if (damageable != null) {
					damageable.DamageRecieve(transform, GetDamageID(), transform.position, RBody.velocity);
					GameManager.Instance.HitStop.SmallHit();
				}

				RBody.velocity *= 0.1f;
				dashTimer = 0f;
			}

		} else if (collision.collider.CompareTag("Projectile")) {
			//GameManager.Instance.HitStop.BigHit();
			collision.collider.GetComponent<Projectile>().Deflect(mouseDirection.forward, GetDamageID());

		}
	}

	#endregion ^ Unity Methods


	#region Implementation Methods

	public override void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {
		throw new System.NotImplementedException();
	}

	#endregion ^ Implementation Methods

	#region Public Events

	public void OnKickEvent() {
		if (legActionController.FrontKick()) {
			RBody.velocity *= 0.5f;
			RBody.angularVelocity *= 0.5f;
			RBody.AddForce(mouseDirection.forward * dashPower * 0.65f, ForceMode.Impulse);
		}

	}

	public void OnDashEvent() {
		if (dashTimer > 0) return;

		//RBody.AddTorque(transform.up * 10, ForceMode.Force);

		//speedVector += mouseDirection.forward * dashPower;
		//speedVector.y = 0;

		RBody.AddTorque(transform.up * 5, ForceMode.Force);
		RBody.AddForce(mouseDirection.forward * dashPower, ForceMode.Impulse);

		//RBody.angularVelocity += mouseDirection.up;
		DashActivatedEvent.Invoke(dashCooldown);
		AudioManager.Instance.PlayAudio(audioSource, Audio.Player.DASH);
		dashTimer = dashCooldown;
	}

	#endregion ^ Public Events

	#region Helper Methods

	#endregion ^ Helper Methods
}