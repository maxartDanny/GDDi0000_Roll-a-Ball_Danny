using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class PlayerController : MortalController {

	#region Variables

	[SerializeField] private float speed = 1;

	[SerializeField] private PlayerLegActionController legActionController;

	[SerializeField] private PlayerInputHandler inputs;

	[SerializeField] private Transform mouseDirection;

	[SerializeField] private Vector3 speedVector = new Vector3();

	private float dashPower = 25f;
	private float dashTime = 0;
	private float dashCooldown = 0.5f;

	#endregion ^ Variables


	#region Events

	[SerializeField] private FloatEvent DashActivatedEvent = new FloatEvent();

	#endregion ^ Events


	#region Unity Methods

	private void Start() {
		RBody.maxAngularVelocity = float.MaxValue;
	}

	private void FixedUpdate() {

		Vector3 movement = new Vector3(inputs.Horizontal, 0, inputs.Vertical) * speed * Time.fixedDeltaTime;

		speedVector += movement;
		speedVector.y = RBody.velocity.y;

		//Debug.LogFormat("Speed Vector: {0}", speedVector.magnitude);

		RBody.velocity = speedVector;

		speedVector *= 0.9f;

	}

	private void OnDestroy() {
		DashActivatedEvent?.RemoveAllListeners();
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Enemy")) {

			Debug.LogFormat("Hit enemy {0}", speedVector.magnitude);

			if (speedVector.magnitude >= 9f) {

				IDamageable damageable = collision.transform.GetComponent<IDamageable>();

				if (damageable != null) {
					damageable.DamageRecieve(transform, GetDamageID(), transform.position, RBody.velocity);
				}

				speedVector *= 0.1f;
			}

		} else if (collision.collider.CompareTag("Projectile")) {

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
		}
		RBody.AddTorque(transform.up * 10, ForceMode.Force);

	}

	public void OnDashEvent() {

		if (dashTime > Time.time) return;

		//RBody.AddTorque(transform.up * 10, ForceMode.Force);

		speedVector += mouseDirection.forward * dashPower;
		speedVector.y = 0;

		//RBody.AddForce(mouseDirection.forward * 1000);

		//RBody.angularVelocity += mouseDirection.up;

		dashTime = Time.time + dashCooldown;

		DashActivatedEvent?.Invoke(dashCooldown);

	}

	#endregion ^ Public Events

	#region Helper Methods

	#endregion ^ Helper Methods
}