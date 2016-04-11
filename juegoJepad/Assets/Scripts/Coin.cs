using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		GameMaster.current.AddCoin();
		Destroy (this.gameObject);
	}
}
