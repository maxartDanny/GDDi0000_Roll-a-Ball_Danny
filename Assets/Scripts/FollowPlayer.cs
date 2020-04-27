using UnityEngine;

/// <summary>
///
/// </summary>
public class FollowPlayer : MonoBehaviour {

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

	#endregion ^ Unity Methods


	#region Public Methods

	private void FixedUpdate() {
		myTransform.position = target.position;

		Vector3 dir = target.position - prevPos;
		if (!dir.Equals(Vector3.zero)) {
			myTransform.rotation = Quaternion.LookRotation(dir);
			prevPos = target.position;
		}
	}

	#endregion ^ Public Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}