using UnityEngine;
using System.Collections;

public class JumperZombieAI : MonoBehaviour {
	public float walkSpeed;
	public float maxFallSpeed;
	public float jumpImpulse;

	public Transform groundCheckPoint;
	public LayerMask whatIsGround;
	public int damage;

	public Transform attackPoint;
	public LayerMask whatToAttack;

	public GameObject coinPrfb;

	private Transform target;
	private Rigidbody2D body;
	private Vector2 movement;

	private float horInput;
	private bool iAmIntheGround;
	private bool facingRight;

	private Animator anim;

	private float time;
	private float timeToAttack;

	// =============================
	void Start () {

		this.body = this.GetComponent<Rigidbody2D>();
		this.movement = new Vector2();
		this.iAmIntheGround = false;

		this.anim = this.GetComponent<Animator>();
		this.facingRight = true;

		this.time = 0;
		this.timeToAttack = 1;

		GameObject tmp = GameObject.FindGameObjectWithTag("Player");
		if(tmp != null){
			this.target = tmp.transform;
		}
	}
	// =============================
	void Update () {
		// search for the player
		if(this.target){
			if(this.transform.position.x < this.target.position.x){
				this.horInput = 1;
			}else if(this.transform.position.x > this.target.position.x){
				this.horInput = -1;
			}
		}else{
			this.horInput = 0;
		}
		// Trun arround
		if ((this.horInput < 0) && (this.facingRight)){
			this.facingRight = false;
			this.Flip();

		}else if((this.horInput > 0) && (!this.facingRight)){
			this.facingRight = true;
			this.Flip();
		}
		// animator update
		this.anim.SetFloat("HorSpeed", Mathf.Abs(this.body.velocity.x));
		this.anim.SetFloat("VertSpeed", Mathf.Abs (this.body.velocity.y));

		// Ground Check
		if(Physics2D.OverlapCircle(this.groundCheckPoint.position, 0.02f, this.whatIsGround)){
			this.iAmIntheGround = true;
		}else {
			this.iAmIntheGround = false;
		}
	}
	// =============================
	void FixedUpdate(){
		this.movement = this.body.velocity;

		this.time += Time.deltaTime;
		// Wait for attack
		if(this.time > this.timeToAttack){
			this.time = 0;
			this.Detect();
		}

		// Movimiento horizontal
		this.movement.x = horInput * walkSpeed;

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
	void Detect(){
		Collider2D tmp = Physics2D.OverlapCircle(this.attackPoint.position, 0.02f, this.whatToAttack);
		if(tmp){
			if(tmp.gameObject.CompareTag("Player")){
				tmp.gameObject.SendMessage("Hurt", this.damage);
			}else{
				if(this.iAmIntheGround){
					this.movement.y = jumpImpulse;
				}
			}
		}
	}
	// =============================
	public void OnGetKill(){
		int rnd = Random.Range(0,2);
		if(rnd == 1){
			Instantiate(this.coinPrfb, this.transform.position, Quaternion.identity);
		}
		GameMaster.current.AddPuntuation(100);
	}
}
