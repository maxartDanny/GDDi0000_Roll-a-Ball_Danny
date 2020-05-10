using UnityEngine;

/// <summary>
///
/// </summary>
public class FollowPlayer : MonoBehaviour {

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
		myTransform.position = Vector3.Lerp(myTransform.position, target.MyTransform.position, 0.85f);
	}

	#endregion ^ Unity Methods


}