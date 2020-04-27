using UnityEngine;

/// <summary>
///
/// </summary>
public class CameraController : MonoBehaviour {

	#region Variables


	[SerializeField] private Transform player;

	private Vector3 offset;

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		offset = transform.position - player.position;
	}

	private void LateUpdate() {
		transform.position = player.position + offset;
	}

	#endregion ^ Unity Methods

}