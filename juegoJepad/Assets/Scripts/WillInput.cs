using UnityEngine;
using System.Collections;

public class WillInput : MonoBehaviour {
	public float walkSpeed;
	public float jumpImpulse;
	public float maxFallSpeed;

	public Transform groundCheckPoint;
	public LayerMask whatIsGround;

	private Rigidbody2D body;
	private Vector2 movement;

	private float horInput;
	private bool jumpInput;
	private bool iAmIntheGround;
	private bool facingRight;

	private Animator anim;

	// =============================
	void Start () {
		this.body = this.GetComponent<Rigidbody2D>();
		this.movement = new Vector2();
		this.iAmIntheGround = false;

		this.anim = this.GetComponent<Animator>();
		this.facingRight = true;
	}
	// =============================
	void Update () {
		this.horInput = InputManager.current.GetHorizontal();
		this.jumpInput = InputManager.current.GetJump();

		if ((this.horInput < 0) && (this.facingRight)){
			this.facingRight = false;
			this.Flip();

		}else if((this.horInput > 0) && (!this.facingRight)){
			this.facingRight = true;
			this.Flip();
		}

		this.anim.SetFloat("HorSpeed", Mathf.Abs(this.body.velocity.x));
		this.anim.SetFloat("VertSpeed", Mathf.Abs (this.body.velocity.y));

		if(Physics2D.OverlapCircle(this.groundCheckPoint.position, 0.02f, this.whatIsGround)){
			this.iAmIntheGround = true;
		}else {
			this.iAmIntheGround = false;
		}
	}
	// =============================
	void FixedUpdate(){
		this.movement = this.body.velocity;

		// Movimiento horizontal
		this.movement.x = horInput * walkSpeed;
		// Salto
		if(this.jumpInput && this.iAmIntheGround){
			this.movement.y = jumpImpulse;
		}
		// Limitacion de la velocidad de caida
		if( !this.iAmIntheGround ){
			if(this.movement.y < this.maxFallSpeed){
				this.movement.y = this.maxFallSpeed;
			}
		}

		this.body.velocity = this.movement;
	}
	// =============================
	void Flip(){
		//Vector3 scale = this.transform.localScale;
		//scale.x *= (-1);
		//this.transform.localScale = scale;
		this.transform.Rotate(Vector3.up, 180);
	}
	// =============================
	public void OnGetKill(){
		GameMaster.current.GameOver();
	}
}
