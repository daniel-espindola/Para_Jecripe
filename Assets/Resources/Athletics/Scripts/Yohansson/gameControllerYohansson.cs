﻿using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;

public class gameControllerYohansson : MonoBehaviour {

	public Text result;

	public PlayfabManager playfabManager;

	public string breakRecord = "";

	private StoreDataContainer sD;
	private enemyBehaviour adversaryScript;
	private enemyBehaviour2 adversary2Script;
	private enemyBehaviour3 adversary3Script;
	private float time1,time2,time3, time4, aux; 
	private string first, second, third, fourth, saux,medal;
	private bool end, save,pF,pS,pT;

	GameObject gameOverCanvas;
	GameObject canvas;

	Text place1;
	Text place2;
	Text place3;
	Text place4;

	public int prizecoins,p,f;

	public GameObject waitCanvas;


	void Start () {	

		adversaryScript = GameObject.Find ("athMale_adversary1").GetComponent<enemyBehaviour>();
		adversary2Script = GameObject.Find ("athMale_adversary2").GetComponent<enemyBehaviour2>();
		adversary3Script = GameObject.Find ("athMale_adversary3").GetComponent<enemyBehaviour3>();

		prizecoins = 0;

		end = false;
		save= false;
		pF=false;
		pS=false;
		pT=false;
		p=0;

		gameOverCanvas = GameObject.Find ("GameOver");
		canvas = GameObject.Find("Canvas");

		if (PlayerPrefs.GetFloat ("bestTime") == 0) {
			PlayerPrefs.SetFloat ("bestTime", 90f);
		}

		//Sounds = GameObject.Find ("Sounds").GetComponent<AthleticsSounds>();

		place1 = GameObject.Find ("FirstPlace").GetComponent<Text>();
		place2 = GameObject.Find ("SecondPlace").GetComponent<Text>();	
		place3 = GameObject.Find ("ThirdPlace").GetComponent<Text>();
		place4 = GameObject.Find ("FourthPlace").GetComponent<Text>();

		result = GameObject.Find ("Result").GetComponentInChildren<Text>();
		gameOverCanvas.SetActive(false);


	}

	void StoreHighscore(float newHighscore)
	{
		float oldHighscore = PlayerPrefs.GetFloat ("bestTime"); 
		if(newHighscore < oldHighscore)
		{
			PlayerPrefs.SetFloat("bestTime", newHighscore);
			breakRecord = " Você quebrou o Record!";
		}

	}

	public void Reload(){

		Application.LoadLevel(Application.loadedLevel);

	}

	public void BackToMenu(){
		Application.LoadLevel ("PlayAthletics");
	}

	void scoreBuilder(){
		gameOverCanvas.SetActive(true);

		place1.text = first;
		place2.text = second;
		place3.text = third;
		place4.text = fourth;

		result.text = "Parabéns, você ganhou "+ 50 +" moedas!";
		Grant();

		end = true;

	}
	public void Grant()
    {
		var request = new AddUserVirtualCurrencyRequest
		{
			VirtualCurrency = "PJ",
			Amount = 50
		};
		PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantVirtualCurrencySuccess, OnError);
	}
	public void GrantVirtualCurrency()
	{
		var request = new AddUserVirtualCurrencyRequest
		{
			VirtualCurrency = "OL",
			Amount = 50
		};
		PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantVirtualCurrencySuccess, OnError);
	}
	void OnGrantVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log("Currency granted!");
		playfabManager.GetVirtualCurrencies();
	}
	void OnError(PlayFabError error)
	{
		Debug.Log("Error: " + error.ErrorMessage);
	}
	void sortedTimes(){

		if (playerBehaviour2.termina ==true && adversaryScript.termina == false && adversary2Script.termina == false && adversary3Script.termina == false){
			pF=true;
			pS= false;
			pT=false;
			if (p==0){
				first = "Yohansson Nascimento";
				second = adversary2Script.adversary.name;
				third = adversaryScript.adversary.name;
				fourth = adversary3Script.adversary.name;
				p=1;
			}
		}
		else if (playerBehaviour2.termina ==true && pF==false && pT==false){
			pF=pT=false;
			pS=true;
			if (p==0){
				first = adversary3Script.adversary.name;
				second = "Yohansson Nascimento";
				third = adversaryScript.adversary.name;
				fourth = adversary2Script.adversary.name;
				p=2;
			}
		}

		else if (pF=false && pS==false){
			pT=true;
			pS=pF=false;
			if (p==0){
				first = adversary3Script.adversary.name;
				second = adversary2Script.adversary.name;
				third = "Yohansson Nascimento";
				fourth = adversaryScript.adversary.name;
				p=3;
			}
		}
		else if (playerBehaviour2.termina ==false && adversaryScript.termina == true && adversary2Script.termina == true && adversary3Script.termina == true)
		{
			pF=pS=pT=false;

			first = adversary3Script.adversary.name;
			second = adversary2Script.adversary.name;
			third = adversaryScript.adversary.name;
			fourth = "Yohansson Nascimento";
			p=4;
		}	
		else if (playerBehaviour2.termina ==false && adversaryScript.termina == false && adversary2Script.termina == false && adversary3Script.termina == false)
		{
			pF=pS=pT=false;
		}
		Debug.Log(p);
		Debug.Log("first"+first);
		Debug.Log("second"+second);
		Debug.Log("third"+third);
		Debug.Log("four"+fourth);
	}

	public void showPrize(){

		if ((p==1)) {
			prizecoins = 1000;
			first = "Yohansson Nascimento";
			second = adversary2Script.adversary.name;
			third = adversaryScript.adversary.name;
			fourth = adversary3Script.adversary.name;


			//medal = "Parabéns você ganhou medalha de ouro e "+prizecoins +" moedas!";
		}
		else if ((p==2)) {
			prizecoins = 700;
			first = adversary3Script.adversary.name;
			second = "Yohansson Nascimento";
			third = adversaryScript.adversary.name;
			fourth = adversary2Script.adversary.name;

			//medal = "Parabéns você ganhou medalha de prata e "+prizecoins +" moedas!";
		}
		else if ((p==3)) {
			prizecoins = 500;
			first = adversary3Script.adversary.name;
			second = adversary2Script.adversary.name;
			third = "Yohansson Nascimento";
			fourth = adversaryScript.adversary.name;

			//medal = "Parabéns você ganhou medalha de bronze e "+prizecoins +" moedas!";;
		}
		else if ((p==4)) {
			//medal = "Não foi dessa vez! Tente mais vezes e conquiste medalhas!";
			prizecoins = 0;
			first = adversary3Script.adversary.name;
			second = adversary2Script.adversary.name;
			third = adversaryScript.adversary.name;
			fourth = "Yohansson Nascimento";
		}

		if( end == true && playerBehaviour2.termina==true && save == false){
			sD = StoreDataContainer.Load();
			sD.storeObjects[0].coin += prizecoins;
			sD.Save();
			save = true;
		}

		result.text = "Parabéns você ganhou "+(playerBehaviour2.bonusnumber+prizecoins) +" moedas!";

	}

	public void GameOverAthletics()
	{
		waitCanvas.SetActive (false);

		showPrize();
		scoreBuilder ();
		gameOverCanvas.SetActive (true);

	}
	void Update(){


		sortedTimes ();

		if (playerBehaviour2.termina==true) {
			StoreHighscore (playerBehaviour2.playertime);



			if (adversaryScript.termina == false || adversary2Script.termina == false || adversary3Script.termina == false) {
				waitCanvas.SetActive (true);

			} else {

				GameOverAthletics();

			}
		}

		while (adversaryScript.adversary.id == adversary2Script.adversary.id || 
			adversary3Script.adversary.id == adversary2Script.adversary.id ||
			adversaryScript.adversary.id == adversary3Script.adversary.id) {

			adversaryScript.adversary.featuresAdversaryYohansson (Random.Range (1,8));
			adversary2Script.adversary.featuresAdversaryYohansson (Random.Range (1,8));
			adversary3Script.adversary.featuresAdversaryYohansson (Random.Range (1,8));

		}


	}

}