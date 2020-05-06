using UnityEngine;

/// <summary>
///
/// </summary>
public class PowerUpsView : MonoBehaviour {

	#region Variables

	[SerializeField] private GameObject slashObject;
	[SerializeField] private GameObject kickObject;

	[SerializeField] private PowerUpFillView dashFill;


	#endregion ^ Variables


	#region Unity Methods



	#endregion ^ Unity Methods


	#region Public Methods

	public void EnableSlash(bool state) {
		slashObject.SetActive(state);
	}

	public void EnableKick (bool state) {
		kickObject.SetActive(state);
	}

	#endregion ^ Public Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}