using UnityEngine;

/// <summary>
///
/// </summary>
public class BossZone : MonoBehaviour {

	#region Variables

	private ZoneArea zone;

	#endregion ^ Variables

	#region Unity Methods

	private void Start() {
		zone = GetComponent<ZoneArea>();
		zone.ClearUpdateEvent.AddListener(OnZoneComplete);
	}

	#endregion ^ Unity Methods

	#region Event Methods

	private void OnZoneComplete(bool clear) {
		if (clear) {
			GameManager.Instance.BossComplete();
		}
	}

	#endregion ^ Event Methods
}