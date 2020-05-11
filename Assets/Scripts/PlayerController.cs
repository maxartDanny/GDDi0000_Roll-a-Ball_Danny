using UnityEngine;
using Audio;
using System.Security.Cryptography;
using UnityEngine.UIElements;

/// <summary>
///
/// </summary>
public class PlayerController : MortalController {

	#region Variables

	[SerializeField] private float speed = 1;

	[SerializeField] private PlayerLegActionController legActionController;

	[SerializeField] private SwordController sword;

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
	public Transform MyTransform => myTransform;
	public override Vector3 Direction => mouseDirection.forward;

	#endregion ^ Properties


	#region Events


	#endregion ^ Events


	#region Unity Methods

	private void Awake() {
		GameManager.Instance.AssignPlayer(this);
		RBody.maxAngularVelocity = float.MaxValue;
		myTransform = transform;
		prevPos = myTransform.position;
	}

	private void FixedUpdate() {

		if (IsDead) return;

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

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Enemy")) {

			//Debug.LogFormat("Hit enemy RB: {0} | calc: {1}", RBody.velocity.magnitude, calcVelocity.magnitude);

			if (calcVelocity.magnitude >= 8f) {

				IDamageable damageable = collision.transform.GetComponent<IDamageable>();

				if (damageable != null) {
					damageable.DamageRecieve(transform, MyDamageID(), transform.position, RBody.velocity);
					GameManager.Instance.HitStop.SmallHit();
				}

				RBody.velocity *= 0.1f;
				dashTimer = 0f;
			}

		} else if (collision.collider.CompareTag("Projectile")) {

			Projectile projectile = collision.collider.GetComponent<Projectile>();
			if (projectile.DamageType == MyDamageID()) return;
			DamageRecieve(collision.transform, projectile.DamageType, projectile.Position, projectile.Velocity);
			Destroy(projectile.gameObject);
			GameManager.Instance.HitStop.BigHit();
		}
	}

	#endregion ^ Unity Methods


	#region Implementation Methods

	public override void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {
		if (IsDead) return;

		if (damageType != MyDamageID()) {

			Vector3 myPos = myTransform.position;
			sourcePos.y = myPos.y;
			Vector3 impactDir = (myPos - sourcePos).normalized;
			float impact = Mathf.Clamp(velocity.magnitude, 0, 2);

			RBody?.AddForce(impactDir * (2 + impact), ForceMode.Impulse);

			Health--;
			AudioManager.Instance.PlayAudio(audioSource, Audio.Enemy.HIT);
		}
	}

	#endregion ^ Implementation Methods

	#region Event Methods

	public void OnKickEvent() {
		if (legActionController.FrontKick()) {
			RBody.velocity *= 0.5f;
			RBody.angularVelocity *= 0.5f;
			RBody.AddForce(mouseDirection.forward * dashPower * 0.65f, ForceMode.Impulse);
		}

	}

	public void OnDashEvent() {
		if (IsDead || dashTimer > 0) return;

		//RBody.AddTorque(transform.up * 10, ForceMode.Force);

		//speedVector += mouseDirection.forward * dashPower;
		//speedVector.y = 0;

		RBody.AddTorque(transform.up * 5, ForceMode.Force);
		RBody.AddForce(mouseDirection.forward * dashPower, ForceMode.Impulse);

		//RBody.angularVelocity += mouseDirection.up;
		AudioManager.Instance.PlayAudio(audioSource, Audio.Player.DASH);
		dashTimer = dashCooldown;
	}

	public void OnSlashEvent() {
		sword.DoSlash(mouseDirection.forward);
	}

	#endregion ^ Event Methods


	#region Public Methods

	public void Respawn(Transform location) {

		RBody.angularVelocity *= 0;
		RBody.velocity *= 0;

		if (location == null) {
			myTransform.SetPositionAndRotation(new Vector3(0, myTransform.position.y, 0), Quaternion.identity);
		} else {
			myTransform.SetPositionAndRotation(
				new Vector3(location.position.x, myTransform.position.y, location.position.z), location.rotation);
		}

		prevPos = myTransform.position;
		calcVelocity *= 0;
		Health = 1;
	}

	#endregion ^ Public Methods
}