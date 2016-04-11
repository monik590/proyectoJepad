using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
	public GameObject[] weapons;
	public int acutalWeapon;

	// ==========================
	void Start () {
		this.acutalWeapon = 0;
		this.ChangeWeapon();
	}
	
	// ==========================
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			this.acutalWeapon = 0;
			this.ChangeWeapon();

		}else if(Input.GetKeyDown(KeyCode.Alpha2)){
			this.acutalWeapon = 1;
			this.ChangeWeapon();

		}else if(Input.GetKeyDown(KeyCode.Alpha3)){
			this.acutalWeapon = 2;
			this.ChangeWeapon();
		}
	}
	// ==========================
	void ChangeWeapon(){
		for(int i = 0; i < this.weapons.Length;i++){
			if(i == this.acutalWeapon){
				this.weapons[i].SetActive(true);
			}else{
				this.weapons[i].SetActive(false);
			}
		}
	}
	// ===========================
	void Refill(){
		for(int i = 0; i < this.weapons.Length;i++){
			if(this.weapons[i].activeSelf){
				Weapon w = this.weapons[i].GetComponent<Weapon>();
				w.ammunition = w.maxAmmunition;
				w.UpdateAmmoText();
			}
		}
	}
}