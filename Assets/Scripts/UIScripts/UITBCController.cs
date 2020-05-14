using UnityEngine;

/// <summary>
///
/// </summary>
public class UITBCController : MonoBehaviour {


	[SerializeField] private GameObject memePanel;

	private void Awake() {
		GameManager.Instance.AssignTBC(this);
	}

	public void EnablePanel(bool state) {
		memePanel.SetActive(state);
	}

}