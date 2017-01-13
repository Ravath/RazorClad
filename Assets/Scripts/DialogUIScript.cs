using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogUIScript : MonoBehaviour {
	
	public Sprite[] personnages;//les portraits de tous les personnages du jeu

	private Repartee[] repartees;

	private Image LeftImage;
	private Image RightImage;
	private Image LeftPortraitBox;
	private Image RightPortraitBox;
	private Text TextDisplayer;

	private int avancee = 0;

	public void Awake() {
		LeftPortraitBox = GameObject.Find("PortraitBox1").GetComponent<Image>();
		RightPortraitBox = GameObject.Find("PortraitBox2").GetComponent<Image>();
		LeftImage = GameObject.Find("Portrait1").GetComponent<Image>();
		RightImage = GameObject.Find("Portrait2").GetComponent<Image>();
		TextDisplayer = GameObject.Find("Text").GetComponent<Text>();
	}

	void Update () {
		if(!Input.anyKeyDown) { return; }//attente d'un signal pour continuer le dialogue.
		if(repartees == null) {//si aucune ligne de dialogue, ne rien afficher.
			SetDescriptive();
			SetText("");
			return;
		}
		//fin préconditions
		if(avancee >= repartees.Length) {//fin du dialogue
			GameManager.Instance.EndDialog();
		} else {//afficher la réplique
			SetRepartee(repartees[avancee++]);
		}

	}
	/// <summary>
	/// Assigne un dialogue à jouer dans l'UI.
	/// </summary>
	/// <param name="repartees">Les répliques du dialogue.</param>
	public void SetDialog( Repartee[] repartees ) {
		this.repartees = repartees;
		SetRepartee(repartees[0]);
		avancee = 1;
	}
	/// <summary>
	/// Affiche la réplique désignée.
	/// </summary>
	/// <param name="repartee">Réplique à afficher.</param>
	private void SetRepartee( Repartee repartee) {
		if(repartee.Locuteur == PersonnagesEnum.Description)
			SetDescriptive();
		else
			SetSpeaker(repartee.Locuteur, repartee.LeftSide);
		SetText(repartee.Message);
	}
	/// <summary>
	/// Le texte à afficher dans l'UI.
	/// </summary>
	/// <param name="text">Le texte.</param>
	private void SetText( string text ) {
		TextDisplayer.text = text;
	}
	/// <summary>
	/// Désigne un locuteur, et sa position dans l'UI.
	/// </summary>
	/// <param name="personnage">Identifiant du personnage à afficher.</param>
	/// <param name="leftSide">Coté où afficher.</param>
	private void SetSpeaker( PersonnagesEnum personnage, bool leftSide = true ) {
		Image UsedImage = leftSide ? LeftImage : RightImage;
		UsedImage.sprite = personnages[(int)personnage];
		RightPortraitBox.gameObject.SetActive(!leftSide);
		LeftPortraitBox.gameObject.SetActive(leftSide);
	}
	/// <summary>
	/// Cache les portraits pour signifier que le texte n'est dit par personne.
	/// </summary>
	private void SetDescriptive() {
		RightPortraitBox.gameObject.SetActive(false);
		LeftPortraitBox.gameObject.SetActive(false);
	}
}
