using UnityEngine;
using UnityEngine.Events;

/// <summary>
///
/// </summary>
public class PlayerInputHandler : MonoBehaviour {

	#region Variables

	private float horizontal = 0;

	private float vertical = 0;

	private bool doKick = false;
	private bool doDash = false;
	private bool doSlash = false;

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

	[SerializeField] private UnityEvent SlashEvent = new UnityEvent();

	#endregion ^ Events


	#region Unity Methods

	private void Start() {
		GameManager.Instance.PlayerDeathEvent.AddListener(OnDeath);
	}

	private void Update() {

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		if (Input.GetKeyDown(KeyCode.Space)) {
			doKick = true;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			doDash = true;
		}

		if (Input.GetKeyDown(KeyCode.Mouse1)) {
			doSlash = true;
		}
	}

	private void FixedUpdate() {

		if (doKick) {
			doKick = false;
			KickEvent?.Invoke();
		}

		if (doDash) {
			doDash = false;
			DashEvent?.Invoke();
		}

		if (doSlash) {
			doSlash = false;
			SlashEvent?.Invoke();
		}

	}

	private void OnDestroy() {
		GameManager.Instance.PlayerDeathEvent.RemoveListener(OnDeath);
	}

	#endregion ^ Unity Methods

	#region Event Methods

	private void OnDeath() {
		doKick = false;
		doDash = false;
		doSlash = false;
	}

	#endregion ^ Event Methods

}