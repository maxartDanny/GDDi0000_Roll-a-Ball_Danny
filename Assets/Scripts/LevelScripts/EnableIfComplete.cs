using UnityEngine;

public class EnableIfComplete : MonoBehaviour {

	private void Awake() {
		gameObject.SetActive(GameManager.Instance.GameCompleted);
	}

}