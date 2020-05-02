using UnityEngine;
using UnityEngine.Events;

public class OnObjectDisable : MonoBehaviour {

    [SerializeField] private UnityEvent ObjectDisableEvent = new UnityEvent();

	private void OnDisable() {
		ObjectDisableEvent?.Invoke();
	}
}