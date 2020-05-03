using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
///
/// </summary>
public class BasicEnemyController : EnemyController {

	#region Variables

	[SerializeField] private float damage = 10;
	[SerializeField] private float projectileSpeed = 100;

	[SerializeField] private GameObject projectilePrefab;

	private float maxImpact = 2;

	private Stack<Action> actionStack = new Stack<Action>();
	[SerializeField] private bool actionStackBusy = false;

	[SerializeField] private Rotate rotateScript;
	[SerializeField] private Color pickUpColour;

	#endregion ^ Variables


	#region Unity Methods

	private void Update() {
		if (actionStackBusy || actionStack.Count == 0) return;

		actionStack.Pop().Invoke();

		actionStackBusy = true;
	}

	private void OnCollisionEnter(Collision collision) {
		if (IsPickup && collision.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
			ScoreManager.Instance.AddScore(1);
		}
	}

	#endregion ^ Unity Methods


	#region Implementation Methods

	public override void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {

		if (IsDead) return;

		Vector3 myPos = transform.position;
		sourcePos.y = myPos.y;
		float impact = Mathf.Clamp(velocity.magnitude, 0, maxImpact);

		RBody?.AddForce(((Vector3.up * 0.5f) + velocity.normalized).normalized * (damage + impact), ForceMode.Impulse);

		Health--;

		CheckHealth();

		actionStack.Push(() => Attack(other));
	}

	#endregion ^ Implementation Methods


	#region Helper Methods

	private void CheckHealth() {
		if (IsDead) {
			actionStack.Push(Death);
		}
	}

	private void Attack(Transform target) {
		StartCoroutine(AttackSequence(target, 0.8f));
	}


	private void Death() {
		StartCoroutine(nameof(DeathSequence));
	}

	private IEnumerator AttackSequence(Transform target, float waitTime) {
		yield return new WaitForSeconds(waitTime);

        Vector3 dir = (target.position - transform.position).normalized * transform.localScale.x;
        Quaternion look = Quaternion.LookRotation(dir);

        GameObject projectile = Instantiate(projectilePrefab, transform.position + dir, look) as GameObject;

        projectile.GetComponent<Projectile>().Initialize(target, GetDamageID(), projectileSpeed);

		ActionComplete();

	}

	private IEnumerator DeathSequence() {
		yield return null;

		rotateScript.GetComponent<Renderer>().material.SetColor("_BaseColor", pickUpColour);
		rotateScript.IsRotating = true;

		IsPickup = true;

		ActionComplete();
	}

	private void ActionComplete() { actionStackBusy = false; }

	#endregion ^ Helper Methods
}