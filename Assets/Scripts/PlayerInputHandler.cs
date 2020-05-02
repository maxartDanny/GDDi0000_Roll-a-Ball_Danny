using UnityEngine;
using UnityEngine.Events;

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


	#region Events


	[SerializeField] private UnityEvent KickEvent = new UnityEvent();

	[SerializeField] private UnityEvent DashEvent = new UnityEvent();

	#endregion ^ Events


	#region Unity Methods

	private void FixedUpdate() {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		if (Input.GetKeyDown(KeyCode.Space)) KickEvent?.Invoke();

		if (Input.GetMouseButtonDown(0)) DashEvent?.Invoke();
	}

	#endregion ^ Unity Methods


	#region Public Methods



	#endregion ^ Public Methods


	#region Helper Methods



	#endregion ^ Helper Methods
}