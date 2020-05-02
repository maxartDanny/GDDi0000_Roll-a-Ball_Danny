using UnityEngine;

/// <summary>
///
/// </summary>
public class FollowPlayerDirection : MonoBehaviour {

	#region Variables

	[SerializeField] private Transform target;

	private Transform myTransform;

	private Vector3 prevPos = new Vector3();

	#endregion ^ Variables


	#region Unity Methods

	private void Awake() {
		myTransform = transform;
		prevPos = -target.forward;
	}

	private void FixedUpdate() {

		Vector3 dir = target.position - prevPos;
		if (dir.sqrMagnitude > 0.000001f) {
			myTransform.rotation = Quaternion.LookRotation(dir, Vector3.up);
			prevPos = target.position;
		}
	}

	#endregion ^ Unity Methods

}