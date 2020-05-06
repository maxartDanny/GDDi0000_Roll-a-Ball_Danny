using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class BasicEnemyController : EnemyController {

	#region Variables

	[SerializeField] protected float impactScale = 10;

	protected float maxImpact = 2;

	protected Stack<Action> actionStack = new Stack<Action>();
	[SerializeField] protected bool actionStackBusy = false;

	[SerializeField] protected EnemyVisualsHandler visuals;

	#endregion ^ Variables


	#region Unity Methods

	protected void Start() {
		CheckHealth();
	}

	protected virtual void Update() {
		if (actionStackBusy || actionStack.Count == 0) return;

		actionStack.Pop().Invoke();

		actionStackBusy = true;
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		if (IsPickup && collision.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
			ScoreManager.Instance.AddScore(1);
		} else if (collision.gameObject.CompareTag("Projectile")) {
			OnProjectileCollide(collision.gameObject.GetComponent<Projectile>());
		}
	}

	#endregion ^ Unity Methods


	#region Implementation Methods

	public override void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity) {

		if (IsDead) return;

		Vector3 myPos = transform.position;
		sourcePos.y = myPos.y;
		Vector3 impactDir = (myPos - sourcePos).normalized;
		float impact = Mathf.Clamp(velocity.magnitude, 0, maxImpact);

		RBody?.AddForce(((Vector3.up * 0.5f) + impactDir).normalized * (impactScale + impact), ForceMode.Impulse);

		Health--;

		CheckHealth();

	}

	#endregion ^ Implementation Methods


	#region Helper Methods

	private void CheckHealth() {
		if (IsDead) {
			visuals.SetInteractable(false);
			actionStack.Push(Death);
		}
	}

	private void Death() {
		StartCoroutine(nameof(DeathSequence));
	}

	private IEnumerator DeathSequence() {
		yield return null;

		visuals.SetPickup();

		IsPickup = true;

		ActionComplete();
	}

	protected void ActionComplete() { actionStackBusy = false; }

	protected virtual void OnProjectileCollide(Projectile projectile) {

		Debug.LogFormat("{0} deflected projectile", name);
		projectile.Deflect(GameManager.Instance.PlayerPosition - projectile.transform.position, GetDamageID());

	}

	#endregion ^ Helper Methods
}