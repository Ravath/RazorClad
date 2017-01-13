using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

	private Image currentHealthBar;

	// Use this for initialization
	void Awake () {
		currentHealthBar = gameObject.GetComponentInChildren<Image>();
    }
	/// <summary>
	/// maj de la barre.
	/// </summary>
	/// <param name="percentage">valeur comprise entre 0(vide) et 1(remplie).</param>
	public void SetBar( float percentage ) {
		Vector3 scale = currentHealthBar.rectTransform.localScale;
		scale.x = Mathf.Clamp(percentage, 0, 1);
		currentHealthBar.rectTransform.localScale = scale;
    }
}
