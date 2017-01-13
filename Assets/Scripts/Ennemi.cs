using UnityEngine;
using System.Collections;

public class Ennemi : Unit {

	//private Attack defenseDamage = new Attack() { damage = 5 };

	private bool rigth = true;

	void Update() {
		HealthCheck();
		if(Random.Range(0, 10) == 0)//déambule aléatoirement
			rigth = !rigth;
        Move(1f * (rigth? 1:-1));
	}
	
	//void OnCollisionEnter2D( Collision2D col ) {
	//	if(col.gameObject.tag == "Player") {
	//		PlayerController player = col.gameObject.GetComponent<PlayerController>();
	//		Vector2 dir = transform.position - player.gameObject.transform.position;
	//		player.GetHit(defenseDamage, dir);
	//		//GetComponent<SpriteRenderer>().flipY = true;
	//	}
	//}
	//void OnCollisionExit2D( Collision2D col ) {
		//if(col.gameObject.tag == "Player") {
		//	GetComponent<SpriteRenderer>().flipY = false;
		//}
	//}
}
