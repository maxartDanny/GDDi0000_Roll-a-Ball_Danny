using UnityEngine;

/// <summary>
///
/// </summary>
public class PlayerInputHandler : MonoBehaviour {

	#region Variables

	private float horizontal = 0;

	private float vertical = 0;

	public float Vertical {
		get { return vertical; }
		set { vertical = value; }
	}

	public float Horizontal {
		get { return horizontal; }
		set { horizontal = value; }
	}


	#endregion ^ Variables


	#region Unity Methods

	private void FixedUpdate() {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");


	}

	#endregion ^ Unity Methods


	#region Public Methods



	#endregion ^ Public Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}