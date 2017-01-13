using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : Unit {

	private Animator animator;

	//attaque
	public GameObject frontSlash;
	public Attack basicAttack;
	private TimedEffect Attacking = new TimedEffect();
	//private float attackTimer;
	//private bool isAttacking = false;

	public HealthBarScript HealthBar;

	protected override void Start() {
		base.Start();
		animator = GetComponent<Animator>();
		frontSlash.SetActive(false);
		Invincible.duration = 0.5f;
    }

	void AttackCheck() {
		if(!Attacking.IsActive && IsFireButtonPressed(1)) {
			animator.SetTrigger("Attaque");
			Attacking.duration = basicAttack.cooldown;
			Attacking.StartEffect();
			//isAttacking = true;
			//attackTimer = basicAttack.cooldown;
			frontSlash.SetActive(true);
		} else if(!Attacking.IsActive) {
			//attackTimer -= Time.deltaTime;
			//if(attackTimer < 0) {
			//	isAttacking = false;
				frontSlash.SetActive(false);
			//}
        }
	}

	void FixedUpdate() {
		HealthCheck();
		GroundCheck();
		AttackCheck();
		//maj bare de vie
		HealthBar.SetBar( (float)currentHealth / (float)health);
		//récupérer inputs & components
		float moveHorizontal = Input.GetAxis("Horizontal");
		Move(moveHorizontal);
		if(Input.GetButtonDown("Jump")) {
			Jump();
		} else if(Input.GetButtonUp("Jump")) {
			Jumping.EndEffect();
        } else if(Input.GetButton("Jump") && Jumping.IsActive) {
			KeepJumping();
		}
		animator.SetFloat("vSpeed", rigidbody.velocity.y);
		animator.SetBool("IsGrounded", isGrounded);
		animator.SetBool("IsRunning", moveHorizontal != 0);
	}

	bool IsFireButtonPressed(int num) {
		return Input.GetButtonDown("Fire" + num.ToString());
	}
}