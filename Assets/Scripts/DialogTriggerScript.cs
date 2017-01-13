using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogTriggerScript : MonoBehaviour {

	public string DialogCode;

	void OnTriggerEnter2D( Collider2D col ) {
		if(col.tag == "Player") {
			GameManager.Instance.StartDialog(DialogCode);
			GameObject.Destroy(gameObject);
		}
	}
}
