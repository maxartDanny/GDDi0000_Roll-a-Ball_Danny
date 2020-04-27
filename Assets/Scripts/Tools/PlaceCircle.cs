using UnityEngine;

/// <summary>
///
/// </summary>
[ExecuteInEditMode]
public class PlaceCircle : MonoBehaviour {

	#region Variables

	[SerializeField] private Transform[] objects;

	[SerializeField] private float radius;

	[SerializeField] private bool update;

	#endregion ^ Variables


	#region Unity Methods

	private void Update() {
		if (update) {
			update = false;
			Place();
		}
	}

	#endregion ^ Unity Methods


	#region Public Methods



	#endregion ^ Public Methods


	#region Helper Methods
	private void Place() {
		int length = objects.Length;
		float rads = 2f * Mathf.PI / length;

		Vector3 pos;
		for (int i = 0; i < length; i++) {
			if (objects[i] != null) {
				pos.x = radius * Mathf.Cos(rads * i);
				pos.y = objects[i].position.y;
				pos.z = radius * Mathf.Sin(rads * i);

				objects[i].position = pos;
			}
		}
	}


	#endregion ^ Helper Methods
}