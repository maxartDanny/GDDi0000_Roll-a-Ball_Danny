using UnityEngine;

/// <summary>
///
/// </summary>
public class FollowPlayerDirection : MonoBehaviour {

	#region Variables

	[SerializeField] private PlayerController target;

	private Transform myTransform;

	private Vector3 prevPos = new Vector3();

	#endregion ^ Variables


	#region Unity Methods

	private void Awake() {
		myTransform = transform;
		prevPos = -target.MyTransform.forward;
	}

	private void FixedUpdate() {
		if (target.IsDead) return;

		Vector3 dir = target.MyTransform.position - prevPos;
		if (dir.sqrMagnitude > 0.000001f) {
			myTransform.rotation = Quaternion.LookRotation(dir, Vector3.up);
			prevPos = target.MyTransform.position;
		}
	}

	#endregion ^ Unity Methods

}