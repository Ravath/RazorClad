using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private DialogCollection dialogues;
	public GameObject Player;
	public GameObject DialogUI;

	#region Singleton
	public static GameManager Instance = null;
	#endregion

	public int currentLevel = 1;

	void Awake() {
		#region Gestion singleton
		if(Instance == null) {
			Instance = this;
			Init();
		} else if(Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject); 
		#endregion
	}
	/// <summary>
	/// Initialise les propriétés du GameManager
	/// </summary>
	private void Init() {
		dialogues = DialogCollection.Load("Dialogues");
		DialogUI = Instantiate(DialogUI);
		DialogUI.SetActive(false);
	}

	#region Gestion dialogue
	/// <summary>
	/// Commence un dialogue. Interromp pour cela le control du personnage, et affiche l'UI.
	/// </summary>
	/// <param name="code">Code du dialogue à jouer</param>
	public void StartDialog( string code ) {
		Player.GetComponent<PlayerController>().enabled = false;
		DialogUI.SetActive(true);
		DialogUIScript dialogUIScript = DialogUI.GetComponent<DialogUIScript>();
		foreach(Dialog iDialog in dialogues.Dialogs) {
			if(iDialog.code == code) {
				dialogUIScript.SetDialog(iDialog.Reparties.ToArray());
				break;
			}
		}
	}
	/// <summary>
	/// Fin du Dialogue. Rend le control du personnage et cache l'UI.
	/// </summary>
	public void EndDialog() {
		Player.GetComponent<PlayerController>().enabled = true;
		DialogUI.SetActive(false);
	} 
	#endregion
}
