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

	private PlayerController player;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		player = GameManager.Instance.Player;
	}

	private void Update() {
		if (player == null) return;

        timer = Mathf.Clamp(player.DashTimer, 0, player.DashCooldown);

        fill.fillAmount = 1 - timer / player.DashCooldown;
	}

	#endregion ^ Unity Methods


	#region Public Methods


	#endregion ^ Public Methods

}