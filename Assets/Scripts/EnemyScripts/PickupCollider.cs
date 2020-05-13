using Audio;
using UnityEngine;

/// <summary>
///
/// </summary>
public class PickupCollider : MonoBehaviour {


    [SerializeField] private EnemyController owner;

    private void OnTriggerEnter(Collider other) {
        if (owner.IsPickup && other.CompareTag("Player")) {
            OnTrigger();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (owner.IsPickup && other.CompareTag("Player")) {
            OnTrigger();
        }
    }

    private void OnTrigger() {
        transform.parent.gameObject.SetActive(false);
        ScoreManager.Instance.AddScore(1);
        AudioManager.Instance.PlayPickup();
    }

}