﻿using UnityEngine;

/// <summary>
///
/// </summary>
public class HitStopManager : MonoBehaviour {

	#region Variables

	private const float REDUCED_TIME_SCALE = 0.1f;
	private const float NORMAL_TIME_SCALE = 1f;

	private const float SMALL_INCREASE = 0.1f;
	private const float BIG_INCREASE = 0.5f;

	private float linear = 0;
	private float quad = 0;
	private float targetTimeScale = 1f;
	private float prevTimeScale = 1f;

	[SerializeField] private float timeScale = 1;

	private float lerpTime = 0.2f;
	private float lerpTimer = 0f;

	#endregion ^ Variables

	#region Properties

	private float LerpValue => lerpTimer / lerpTime;

	#endregion ^ Properties


	#region Unity Methods

	private void Update() {

		// Lerp time scale
		if (lerpTimer > 0) {
			lerpTimer = Mathf.Clamp(lerpTimer - Time.unscaledDeltaTime, 0, lerpTime);

			//Time.timeScale = Mathf.Lerp(prevTimeScale, targetTimeScale, 1 - LerpValue);
			//if (lerpTimer <= 0)
		}

		if (linear <= 0) return;

		linear = Mathf.Clamp01(linear - Time.unscaledDeltaTime);
		quad = linear * linear;
		//quad = Mathf.Clamp01(quad - Time.unscaledDeltaTime);

		Time.timeScale = 1 - quad;
		timeScale = Time.timeScale;
		//if (quad <= 0)
		//	SetTargetScale(NORMAL_TIME_SCALE);
	}

	#endregion ^ Unity Methods


	#region Public Methods

	public void SmallHit() {
		AddLinear(SMALL_INCREASE);
	}

	public void BigHit() {
		AddLinear(BIG_INCREASE);
	}

	#endregion ^ Public Methods


	#region Helper Methods

	private void AddLinear(float amount) {
		//if (quad <= 0) {
		//	SetTargetScale(REDUCED_TIME_SCALE);
		//}

		linear = Mathf.Clamp01(linear + amount);
		quad = linear * linear;
		SetTargetScale(1 - quad);
	}

	private void SetTargetScale(float target) {
		prevTimeScale = Time.timeScale;
		targetTimeScale = target;
		lerpTimer = lerpTime;
	}

	#endregion ^ Helper Methods
}