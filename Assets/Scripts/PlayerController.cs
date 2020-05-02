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

	[SerializeField] private Text countText;
	[SerializeField] private GameObject winText;

	private const int requiredCount = 12;
	private int count = 0;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		UpdateCountDisplay();
	}

	private void FixedUpdate() {

		Vector3 movement = new Vector3(inputs.Horizontal, 0, inputs.Vertical);

		RBody.AddForce(movement * speed);

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (legActionController.FrontKick()) {
				RBody.velocity *= 0.5f;
				RBody.angularVelocity *= 0.5f;
			}
		}
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