using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

/// <summary>
///
/// </summary>
public class CameraFocus : MonoBehaviour {

	#region Variables

	private float lerpSpeed = 0.2f;

	private const float SET_HEIGHT = 0.5f;

	private const float DIST_THRESHOLD_SQR = 100;

	private Vector3 localOriginalCameraPos;
	private Vector2 playerDistanceSqrBounds = new Vector2(4, 324);

	private Vector3 targetPos;
	private Vector3 cameraTargetPos;

	private PlayerController player;

	[SerializeField] private Transform camera;

	private Transform myTransform;

	private ZoneArea currentZone;
	private List<EnemyController> enemiesInZone;

	#endregion ^ Variables


	#region Unity Methods

	private void Awake() {
		GameManager.Instance.ZoneActivatedEvent.AddListener(OnZoneActivate);

		Debug.LogFormat("Subscribed to zone activated");
		myTransform = transform;

		localOriginalCameraPos = camera.localPosition;
	}

	private void Start() {
		player = GameManager.Instance.Player;
	}

	private void Update() {

		if (player.IsDead) return;

		targetPos = player.MyTransform.position;

		if (currentZone != null) {
			int length = enemiesInZone.Count;
			EnemyController enemy;
			for (int i = 0; i < length; i++) {
				enemy = enemiesInZone[i];

				float distSqr = Vector3.SqrMagnitude(enemy.transform.position - player.MyTransform.position);

				float linear = Mathf.InverseLerp(324, 1, distSqr);
				float weight = Mathf.Lerp(0, 0.4f, linear);

				if (enemy.IsDead) weight *= 0.5f;

				targetPos += (enemy.transform.position - targetPos) * weight;
			}

			if (currentZone.Clear || !currentZone.ZoneActive) {
				currentZone = null;
				enemiesInZone = null;
			}
		}

		targetPos.y = SET_HEIGHT;

		myTransform.position = Vector3.Lerp(myTransform.position, targetPos, lerpSpeed);

		//float sqrDist = Vector3.SqrMagnitude(myTransform.position - targetPos);


	}

	private void OnDestroy() {
		GameManager.Instance.ZoneActivatedEvent.RemoveListener(OnZoneActivate);
	}

	#endregion ^ Unity Methods


	#region Event Methods

	private void OnZoneActivate(ZoneArea zone) {
		Debug.LogFormat("Zone Activated and stored");
		enemiesInZone = zone.Enemies;
		currentZone = zone;
	}

	#endregion ^ Event Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}