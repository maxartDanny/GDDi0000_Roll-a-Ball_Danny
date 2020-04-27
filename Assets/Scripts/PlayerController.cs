using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class PlayerController : MonoBehaviour {

	#region Variables

	[SerializeField] private float speed = 1;

	[SerializeField] private PlayerLegActionController legActionController;

	private Rigidbody rbody;

	[SerializeField] private Text countText;
	[SerializeField] private GameObject winText;

	private const int requiredCount = 12;
	private int count = 0;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		rbody = GetComponent<Rigidbody>();
		UpdateCountDisplay();
	}

	private void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

		rbody.AddForce(movement * speed);

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (legActionController.FrontKick()) {
				rbody.velocity *= 0.5f;
				rbody.angularVelocity *= 0.5f;
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