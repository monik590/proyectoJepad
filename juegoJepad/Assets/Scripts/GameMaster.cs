using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public static GameMaster current;
	public GameObject player;

	public Text puntuationText;
	public GameObject[] spawners;

	public GameObject deathPanel;
	public Text puntDeathText;

	public Text coinText;

	private int coins;
	private Vector3 startPosition;
	private int puntuation;

	private float timeWest;
	private float timeEast;
	private float timeOut;

	// ==============================
	void Awake(){
		GameMaster.current = this;
	}
	// ==============================
	void Start () {
		this.puntuation = 0;
		this.startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		this.UpdatePuntText();

		this.spawners[1].SetActive(false);

		this.timeWest = 0;
		this.timeEast = 0;
		this.timeOut = 3;
		this.coins = 0;
		this.UpdateCoinText();

		this.player = GameObject.FindGameObjectWithTag("Player");
	}
	// ==============================
	void Update(){
		// East Spawner
		if( ! this.spawners[0].activeSelf ){
			this.timeEast += Time.deltaTime;
			
			if(this.timeEast > this.timeOut){
				this.timeEast = 0;
				this.spawners[0].SetActive(true);
			}
		}
		// West Spawner
		if( ! this.spawners[1].activeSelf ){
			this.timeWest += Time.deltaTime;

			if(this.timeWest > this.timeOut){
				this.timeWest = 0;
				this.spawners[1].SetActive(true);
			}
		}
	}
	// ==============================
	void UpdateCoinText(){
		this.coinText.text = ":" + this.coins;
	}
	// ==============================
	void UpdatePuntText(){
		this.puntuationText.text = "- " + this.puntuation + " -";
	}
	// ==============================
	public void AddPuntuation(int amount){
		this.puntuation += amount;
		this.UpdatePuntText();
	}
	// ==============================
	public void GameOver(){
		this.deathPanel.SetActive(true);
		this.puntDeathText.text = "Puntuation: " + this.puntuation;
	}
	// ===============================
	public void LoadScene(int index){
		Application.LoadLevel(index);
	}
	// ===============================
	public void AddCoin(){
		this.coins++;
		this.UpdateCoinText();
	}
	// ===============================
	public void Buy(){
		if(this.coins >= 10){
			this.player.SendMessage("Refill");
			this.coins -= 10;
			this.UpdateCoinText();
		}
	}
}
