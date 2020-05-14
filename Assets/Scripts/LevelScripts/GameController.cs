using UnityEngine;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour {

	#region Unity Methods

	private void Update() {

		if (Input.GetKeyDown(KeyCode.R)) {
			ReloadScene();
		} else if (Input.GetKeyDown(KeyCode.Alpha0)) {
			ClearPlayerPrefs();
		}

	}

	#endregion ^ Unity Methods

	private void ReloadScene() {
		GameManager.Instance.HitStop.ResetTimeScale();
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);

		//if (!Application.isEditor) {
		//	System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program
		//	Application.Quit(); //kill current process
		//}
	}

	private void ClearPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

}