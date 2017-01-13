using UnityEngine;
using System.Collections;


public abstract class Unit : MonoBehaviour {
	public int health;  //max health
	public int armor;	//damage reduction
	public float speed;	//moving speed
	public float jumpPower;//jump power
	public int jumpsNumber = 1;//number of consecutive jumps the unit cant do


	protected int  currentHealth{ get; private set; }//the current health
	protected int  usedJumps	{ get; private set; }//the current number of consecutive jumps
	protected bool facingRight  { get; private set; } //is the unit facing right?
	protected bool isGrounded	{ get; private set; }//is the unit on the ground?
	protected bool isInvincible	{ get; private set; }//is the unit in invincible mode?
	protected TimedEffect Invincible = new TimedEffect() { duration = 0f };//temps pendant lequel l'unité est invincible après s'être fait toucher
	protected TimedEffect Jumping = new TimedEffect() { duration = 0.5f };//temps pdt lequel l'unité peut controler son saut (pour le jouer, en maintenant le bouton appuyé pour monter)

	protected new Collider2D collider;
	protected new Rigidbody2D rigidbody;

	protected virtual void Start() {
		collider = GetComponent<Collider2D>();
		rigidbody = GetComponent<Rigidbody2D>();
		currentHealth = health;
		usedJumps = 0;
		facingRight = true;
		isGrounded = true;
		isInvincible = false;
    }
	/// <summary>
	/// Subit une attaque.
	/// </summary>
	/// <param name="attack">Nature de l'attaque.</param>
	/// <param name="direction">Direction de l'attaque.</param>
	public void GetHit( Attack attack , Vector2 direction ) {
		if(Invincible.IsActive) { return; }
		currentHealth -= attack.damage - Mathf.Max(0,armor - attack.piercing);
		rigidbody.velocity = attack.force * direction;
		Invincible.StartEffect();
		StartCoroutine("Twinkle", Invincible.duration);
	}
	public IEnumerator Twinkle(float time) {
		float delta = 0.1f;
		while(time>0) {
			time -= delta;
			GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(delta);
		}
	}
	protected void GroundCheck() {
		RaycastHit2D[] res = new RaycastHit2D[1];
		isGrounded = collider.Raycast(-Vector2.up, res, collider.bounds.extents.y + 0.1f) > 0;
		if(rigidbody.velocity.y < 0 && isGrounded)//atterissage
			usedJumps = 0;
	}

	protected void HealthCheck() {
		if(currentHealth < 0) {//die
			Destroy(gameObject);
		}	
	}

	protected void Flip() {
		facingRight = !facingRight;
		Vector3 scale = GetComponent<Rigidbody2D>().transform.localScale;
		scale.x *= -1;
		GetComponent<Rigidbody2D>().transform.localScale = scale;
	}

	protected void Move( float xAxis ) {
		Vector2 vel = rigidbody.velocity;
		vel.x = xAxis * speed;
		rigidbody.velocity = vel;
		//orienter le personnage
		if(xAxis < 0 && facingRight)//vers la gauche
			Flip();
		else if(xAxis > 0 && !facingRight)//vers la droite
			Flip();
	}

	protected void Jump() {
		if(isGrounded || usedJumps < jumpsNumber) {
			Vector2 vel = rigidbody.velocity;
			vel.y = jumpPower;
			rigidbody.velocity = vel;
			usedJumps++;
			Jumping.StartEffect();
		}
	}
	protected void KeepJumping() {
		Vector2 vel = rigidbody.velocity;
		vel.y = jumpPower;
		rigidbody.velocity = vel;
	}
}
