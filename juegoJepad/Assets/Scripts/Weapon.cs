using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {
	public LayerMask whatToHit;
	public GameObject lineEffectPrefab;

	public float damage;
	public float shootRatio;
	public int ammunition;
	public int maxAmmunition;

	public Text ammoText;

	private float time;
	private AudioSource shootAudio;

	// ============================
	void Start () {
		this.shootAudio = this.GetComponent<AudioSource>();
	}
	// ============================
	void Update () {
		this.time += Time.deltaTime;

		if( InputManager.current.GetShooting() && (this.time > this.shootRatio) ){
			this.time = 0;
			if(this.ammunition > 0){
				this.Shoot();
				this.shootAudio.Play();
				this.ammunition--;
				this.UpdateAmmoText();
			}
		}

	}
	// ============================
	void Shoot(){
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, 
		                                     this.transform.right, 10, whatToHit);
		
		GameObject tmp = Instantiate(this.lineEffectPrefab) as GameObject;
		LineRenderer line = tmp.GetComponent<LineRenderer>();
		
		if(hit.collider){
			line.SetPosition(0, this.transform.position);
			line.SetPosition(1, hit.point);

			if(hit.collider.gameObject.CompareTag("Zombie")){
				hit.collider.gameObject.SendMessage("Hurt", this.damage);
			}
		}else{
			line.SetPosition(0, this.transform.position);
			line.SetPosition(1, this.transform.position + (this.transform.right*10));
		}
		Destroy(tmp, 0.05f);
	}
	// ============================
	public void UpdateAmmoText(){
		this.ammoText.text = this.gameObject.name + ": " + this.ammunition + "/" + this.maxAmmunition;
	}
	// ==============================
	void OnEnable(){
		this.time = this.shootRatio;
		this.UpdateAmmoText();
	}
}
