using UnityEngine;

/// <summary>
///
/// </summary>
public class SwordController : MonoBehaviour {

	#region Variables

	[SerializeField] private Rigidbody rbody;

	[SerializeField] private Animator swordAnimator;

	[SerializeField] private GameObject swordObject;

	private float duration = 0.9f;
	private float timer = 0;

	#endregion ^ Variables


	#region Properties

	public Rigidbody RBody => rbody;

	#endregion ^ Properties


	#region Unity Methods

	private void Awake() {
		//rbody.maxAngularVelocity = float.MaxValue;
	}

	private void FixedUpdate() {
		if (timer > 0) {
			timer -= Time.fixedDeltaTime;

			if (timer <= 0) {
				//EnableSword(false);
			}
		}
	}

	#endregion ^ Unity Methods


	#region Public Methods

	public void DoSlash(Vector3 direction) {
		if (timer > 0) return;
		transform.rotation = Quaternion.LookRotation(direction);
		swordAnimator.SetTrigger("Slash");
		timer = duration;
	}

	#endregion ^ Public Methods


	#region Helper Methods

	private void EnableSword(bool state) {
		swordObject.SetActive(state);
	}

	#endregion ^ Helper Methods
}