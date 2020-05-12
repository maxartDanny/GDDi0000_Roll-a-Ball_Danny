using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class PowerUpFillView : MonoBehaviour {

	#region Variables


	[SerializeField] private GameObject objectWithInterface;
	private INormTime cooldown;
	[SerializeField] private Image fill;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		cooldown = objectWithInterface.GetComponent<INormTime>();
	}

	private void Update() {
		if (cooldown == null) return;

        fill.fillAmount = 1 - cooldown.NormTime();
	}

	#endregion ^ Unity Methods
}