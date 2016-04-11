using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {
	public float hp;
	public Slider healthBar;

	public GameObject bloodExplosion;

	// ==============================
	void Start () {
	
	}
	// ==============================
	void Update () {
	
	}
	// ==============================
	public void Hurt(float damage){
		this.hp -= damage;

		if(this.hp <= 0){
			this.hp = 0;
			GameObject be = Instantiate(this.bloodExplosion,
			            this.transform.position,
			            Quaternion.identity) as GameObject;

			Destroy(be, 0.5f);

			this.gameObject.SendMessage("OnGetKill");
			Destroy (this.gameObject);
		}

		if(this.healthBar){
			this.healthBar.value = this.hp;
		}
	}
}
