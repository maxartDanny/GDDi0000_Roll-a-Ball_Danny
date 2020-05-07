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

	private void FixedUpdate() {
		myTransform.position = Vector3.Lerp(myTransform.position, target.position, 0.85f);
	}

	#endregion ^ Unity Methods


}