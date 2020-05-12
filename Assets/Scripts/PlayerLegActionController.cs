using System.Collections;
using UnityEngine;

/// <summary>
///
/// </summary>
public class PlayerLegActionController : MonoBehaviour, INormTime {

	#region Variables


	[SerializeField] private GameObject kickAnimator;


	private bool animating = false;

	private float timer = 0;
	private float duration = 0.55f;

	public float NormTime() => Mathf.Clamp01(timer / duration);

	#endregion ^ Variables


	#region Unity Methods


	#endregion ^ Unity Methods


	#region Public Methods

	public bool FrontKick() {
		if (animating) return false;

		animating = true;

		StartCoroutine(nameof(DoAction), kickAnimator);

		return true;
	}

	#endregion ^ Public Methods


	#region Helper Methods

	private IEnumerator DoAction(GameObject leg) {
		leg.SetActive(true);
		timer = duration;

		while (timer > 0) {
			timer -= Time.deltaTime;
			yield return null;
		}
		leg.SetActive(false);
		animating = false;
	}

	#endregion ^ Helper Methods
}