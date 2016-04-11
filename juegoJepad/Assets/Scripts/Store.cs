using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {
	public GameObject buyButton;
	
	void OnTriggerEnter2D(Collider2D other){
		this.buyButton.SetActive(true);
	}
	// ================================
	void OnTriggerExit2D(Collider2D other){
		this.buyButton.SetActive(false);
	}
}
