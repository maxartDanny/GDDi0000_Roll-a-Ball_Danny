using Audio;
using UnityEngine;

/// <summary>
///
/// </summary>
public class BossEnemyController : EnemyController {

	#region Variables

	[SerializeField] private SwordController sword;

	[SerializeField] private ZoneArea bossZone;

	[SerializeField] private AudioSource audioSource;

	private PlayerController player;

	private Transform myTransform;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		player = GameManager.Instance.Player;

		myTransform = transform;

		//InvokeRepeating(nameof(OnAttackAreaTrigger), 1, 1);
		bossZone.ZoneActiveUpdateEvent.AddListener(OnZoneActiveUpdate);
	}

	private void OnDestroy() {
		bossZone.ZoneActiveUpdateEvent.RemoveListener(OnZoneActiveUpdate);
	}

	private void FixedUpdate() {
		if (!bossZone.ZoneActive) return;

		myTransform.LookAt(player.MyTransform.position);
	}

	#endregion ^ Unity Methods


	#region Event Methods

	private void OnZoneActiveUpdate(bool state) {
		if (state) {
			AudioManager.Instance.PlayAudio(audioSource, Audio.Game.TO_BE_CONTINUED_IN);
		}
	}

	#endregion ^ Event Methods


	#region Public Methods

	public void OnAttackAreaTrigger() {
		audioSource.volume = 0.8f;
		AudioManager.Instance.PlayAudio(audioSource, Audio.Game.TO_BE_CONTINUED);
		sword.DoSlash(myTransform.forward);
	}

	public void OnSwordTrigger() {
		GameManager.Instance.TBC.EnablePanel(true);
		GameManager.Instance.HitStop.StopTime();
	}

	#endregion ^ Public Methods

}