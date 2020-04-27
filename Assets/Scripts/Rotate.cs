using UnityEngine;

/// <summary>
///
/// </summary>
public class Rotate : MonoBehaviour {

	#region Variables

	private Transform myTransform;

	private Vector3 rotateValues = new Vector3(15, 30, 45);

	#endregion ^ Variables


	#region Unity Methods

	private void Start() {
		myTransform = transform;
	}

	private void Update() {
		myTransform.Rotate(rotateValues * Time.deltaTime);
	}

	#endregion ^ Unity Methods

}