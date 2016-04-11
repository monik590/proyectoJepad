using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject zombiePrfb;
	public GameObject jumperZombiePrfb;
	public int zombieLimit;

	private float time;
	private float timeToSpawn;
	private float zombieCount;

	// ================================
	void Start () {
		this.time = 0;
		this.timeToSpawn = 1;
	}
	// ================================
	void OnEnable(){
		this.zombieCount = 0;
	}
	// ================================
	void Update () {
		this.time += Time.deltaTime;

		if(this.time > this.timeToSpawn){
			this.time = 0;
			int rnd = Random.Range(0,5);
			if(rnd == 3){
				Instantiate(this.jumperZombiePrfb, this.transform.position, Quaternion.identity);
			}else{
				Instantiate(this.zombiePrfb, this.transform.position, Quaternion.identity);
			}

			this.zombieCount++;
			if(this.zombieCount >= zombieLimit){
				this.gameObject.SetActive(false);
			}
		}
	}
}
