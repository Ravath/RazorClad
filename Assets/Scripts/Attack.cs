using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	public int damage;		//damages to health
	public int piercing=0;	//armor reduction
	public float cooldown;  //time before end of attack
	public int force = 10;  //Force de poussée
	public bool hitPlayer, hitEnnemies;
	//public Animation anim;
	public Collider2D trigger;

	void Awake() {
		//trigger.enabled = false;
		//trigger = gameObject.GetComponent<Collider2D>();
	}

	void OnTriggerEnter2D( Collider2D col ) {
		Unit target = col.gameObject.GetComponent<Unit>();
		if(target == null) { return; }//todo : gérer le cas où ennemi attaque ennemi
		if((hitPlayer && target is PlayerController) || (hitEnnemies && target is Ennemi)) {
			Vector2 dir = target.transform.position - transform.position;//direction de l'attaque
			target.GetHit(this, dir);
		}
	}
}