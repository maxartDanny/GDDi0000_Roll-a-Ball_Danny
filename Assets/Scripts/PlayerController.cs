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

	[SerializeField] private Text countText;
	[SerializeField] private GameObject winText;

	private const int requiredCount = 12;
	private int count = 0;

	[SerializeField] private Vector3 speedVector = new Vector3();

	private float dashTime = 0;
	private float dashCooldown = 3f;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		UpdateCountDisplay();

		RBody.maxAngularVelocity = float.MaxValue;
	}

	private void FixedUpdate() {

		Vector3 movement = new Vector3(inputs.Horizontal, 0, inputs.Vertical) * speed * Time.fixedDeltaTime;

		speedVector += movement;

		RBody.velocity = speedVector;

		speedVector *= 0.9f;

	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("PickUp")) {
			other.gameObject.SetActive(false);
			count++;
			UpdateCountDisplay();
			CheckWinState();
		}
	}

	#endregion ^ Unity Methods


	#region Implementation Methods

	public override void DamageRecieve(IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {
		throw new System.NotImplementedException();
	}

	#endregion ^ Implementation Methods

	#region Public Events

	public void OnKickEvent() {
		if (legActionController.FrontKick()) {
			RBody.velocity *= 0.5f;
			RBody.angularVelocity *= 0.5f;
		}
	}

	public void OnDashEvent() {

		if (dashTime > Time.time) return;

		//RBody.AddTorque(transform.up * 10, ForceMode.Force);

		speedVector += mouseDirection.forward * 50;
		speedVector.y = 0;

		//RBody.AddForce(mouseDirection.forward * 1000);

		//RBody.angularVelocity += mouseDirection.up;

		dashTime = Time.time + dashCooldown;

	}

	#endregion ^ Public Events

	#region Helper Methods

	private void UpdateCountDisplay() {
		countText.text = string.Format("Count: {0}", count);
	}

	private void CheckWinState() {
		if (count < requiredCount) return;
		winText.SetActive(true);
	}



	#endregion ^ Helper Methods
}