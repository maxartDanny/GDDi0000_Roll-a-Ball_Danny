using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class PowerUpFillView : MonoBehaviour {

	#region Variables

	[SerializeField] private Image fill;

	private float time = 0;
	private float timer = 0;

	#endregion ^ Variables


	#region Unity Methods

	private void Update() {
		if (timer > 0) {
			timer = Mathf.Clamp(timer - Time.deltaTime, 0, time);

			fill.fillAmount = 1 - timer / time;
		}
	}

	#endregion ^ Unity Methods


	#region Public Methods

	public void DoFill(float time) {
		this.time = time;
		timer = time;

		fill.fillAmount = 0;
	}

	#endregion ^ Public Methods

}