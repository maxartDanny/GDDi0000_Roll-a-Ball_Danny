using System.Collections;
using UnityEngine;

/// <summary>
///
/// </summary>
public class PlayerLegActionController : MonoBehaviour {

	#region Variables


	[SerializeField] private GameObject kickAnimator;

	private bool animating = false;

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
		yield return new WaitForSeconds(0.55f);
		leg.SetActive(false);
		animating = false;
	}

	#endregion ^ Helper Methods
}