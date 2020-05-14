using UnityEngine;

/// <summary>
///
/// </summary>
public class SwordController : MonoBehaviour, INormTime {

	#region Variables

	[SerializeField] private Animator swordAnimator;

	[SerializeField] private GameObject swordObject;

	private float duration = 0.7f;
	private float timer = 0;

	#endregion ^ Variables


	#region Properties

	public float NormTime() => Mathf.Clamp01(timer / duration);

	#endregion ^ Properties


	#region Unity Methods

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