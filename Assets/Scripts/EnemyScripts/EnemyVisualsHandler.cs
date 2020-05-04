using UnityEngine;

/// <summary>
///
/// </summary>
public class EnemyVisualsHandler : MonoBehaviour {

	#region Variables

	[SerializeField] private Renderer myRenderer;

	[SerializeField] private Rotate rotateScipt;

	private Material material;

	private Color pickupColour = new Color(1, 1, 0);

	#endregion ^ Variables


	#region Unity Methods

	private void Awake() {
		material = myRenderer.material;
	}

	#endregion ^ Unity Methods


	#region Public Methods

	public void SetInteractable(bool state) {
		float h, s, v;
		Color.RGBToHSV(material.GetColor("_BaseColor"), out h, out s, out v);
		s *= 0.5f;
		v *= 0.1f;
		SetColour(Color.HSVToRGB(h, s, v));
	}

	public void SetPickup() {
		SetColour(pickupColour);
		EnableRotation(true);
	}

	public void EnableRotation(bool state) {
		rotateScipt.IsRotating = state;
	}

	#endregion ^ Public Methods


	#region Helper Methods

	private void SetColour(Color colour) {
		material.SetColor("_BaseColor", colour);
	}

	#endregion ^ Helper Methods
}