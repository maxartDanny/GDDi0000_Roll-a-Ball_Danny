using UnityEngine;

/// <summary>
///
/// </summary>
public class FollowMouseDirection : MonoBehaviour {

	#region Variables

	private Camera mainCam;

	[SerializeField] private LayerMask mask;

	private const float rayLength = 1000;

	private RaycastHit hit;

	private Transform myTransform;

	#endregion ^ Variables


	#region Unity Methods

	private void Awake() {
		mainCam = Camera.main;

		myTransform = transform;
	}

	private void FixedUpdate() {

		Ray dir = mainCam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(dir, out hit, rayLength, mask)) {
			Vector3 pos = hit.point;
			pos.y = myTransform.position.y;
			myTransform.LookAt(pos);
		}

	}

	#endregion ^ Unity Methods

}