using UnityEngine;

/// <summary>
///
/// </summary>
public class BossAttackArea : MonoBehaviour {


    [SerializeField] private BossEnemyController owner;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            owner.OnAttackAreaTrigger();
        }
    }

}