using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public static InputManager current;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject jumpButton;
	public GameObject shootButton;
	public Camera touchPadCamera;
	public LayerMask buttonsLayer;

	private float radious;
	private int leftBtnId;
	private int rightBtnId;
	private int jumpBtnId;
	private int shootBtnId;

	private float horInput;
	private bool jumpInput;
	private bool shootInput;
	
	void Awake(){
		current = this;
	}
	// =============================
	void Start () {
		this.radious = 0.3f;
		this.leftBtnId = -1;
		this.rightBtnId = -1;
		this.jumpBtnId = -1;
		this.shootBtnId = -1;

		this.jumpInput = false;
		this.shootInput = false;
	}
	// =============================
	void Update () {
		// PC
		#if UNITY_STANDALONE_WIN

		this.horInput = Input.GetAxis("Horizontal");
		this.jumpInput = Input.GetKey(KeyCode.Space);
		this.shootInput = Input.GetButton("Fire1");
		#endif

		#if UNITY_ANDROID
		// android and IOS

		if(Input.touchCount > 0){
			Touch actual;
			for(int i = 0; i < Input.touchCount;i++){
				actual = Input.GetTouch(i);
				if(actual.phase != TouchPhase.Stationary){

					this.TouchProcess(actual);
				}
			}
		}
		#endif
	}
	// =============================
	void TouchProcess(Touch touch){
		Vector3 position = this.touchPadCamera.ScreenToWorldPoint(touch.position);
		Collider2D collider = Physics2D.OverlapCircle(position, this.radious, this.buttonsLayer);

		if(this.leftBtnId == touch.fingerId){
			if( (collider == null) ||
			   (collider.gameObject != this.leftButton) ||
			   (touch.phase == TouchPhase.Ended) ){

				this.leftBtnId = -1;
				this.horInput = 0;
			}
		}
		if(this.rightBtnId == touch.fingerId){
			if( (collider == null) ||
			   (collider.gameObject != this.rightButton) ||
			   (touch.phase == TouchPhase.Ended) ){
				
				this.rightBtnId = -1;
				this.horInput = 0;
			}
		}
		if(this.jumpBtnId == touch.fingerId){
			if( (collider == null) ||
			   (collider.gameObject != this.jumpButton) ||
			   (touch.phase == TouchPhase.Ended) ){
				
				this.jumpBtnId = -1;
				this.jumpInput = false;
			}
		}
		if(this.shootBtnId == touch.fingerId){
			if( (collider == null) ||
			   (collider.gameObject != this.shootButton) ||
			   (touch.phase == TouchPhase.Ended) ){
				
				this.shootBtnId = -1;
				this.shootInput = false;
			}
		}
		// nuevo dedo
		if(touch.phase != TouchPhase.Ended){
			if(collider != null){
				if(collider.gameObject == this.leftButton){
					this.leftBtnId = touch.fingerId;
					this.horInput = -1;

				}else if(collider.gameObject == this.rightButton){
					this.rightBtnId = touch.fingerId;
					this.horInput = 1;

				}else if(collider.gameObject == this.jumpButton){
					this.jumpBtnId = touch.fingerId;
					this.jumpInput = true;

				}else if(collider.gameObject == this.shootButton){
					this.shootBtnId = touch.fingerId;
					this.shootInput = true;
				}
			}
		}
	}
	// =============================
	public bool GetJump(){
		return this.jumpInput;
	}
	// =============================
	public float GetHorizontal(){
		return this.horInput;
	}
	// =============================
	public bool GetShooting(){
		return this.shootInput;
	}
}
