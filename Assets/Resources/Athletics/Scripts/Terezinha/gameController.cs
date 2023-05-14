using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;

public class gameController : MonoBehaviour {

	public PlayfabManager playfabManager;

	public Text result;

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
		
		adversaryScript = GameObject.Find ("adversary").GetComponent<enemyBehaviour>();
		adversary2Script = GameObject.Find ("adversary2").GetComponent<enemyBehaviour2>();
		adversary3Script = GameObject.Find ("adversary3").GetComponent<enemyBehaviour3>();

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

		result.text = "Parabéns, você ganhou "+(playerBehaviour2.bonusnumber+prizecoins) +" moedas!";


		end = true;
		
	}

	void sortedTimes(){

		if (playerBehaviour2.termina ==true && adversaryScript.termina == false && adversary2Script.termina == false && adversary3Script.termina == false){
			pF=true;
			pS= false;
			pT=false;
			if (p==0){
			first = "Terezinha Guilhermina e \nRafael Lazarini";
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
			second = "Terezinha Guilhermina e \nRafael Lazarini";
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
			third = "Terezinha Guilhermina e \nRafael Lazarini";
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
			fourth = "Terezinha Guilhermina e \nRafael Lazarini";
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
			prizecoins = 100;
			first = "Terezinha Guilhermina e \nRafael Lazarini";
			second = adversary2Script.adversary.name;
			third = adversaryScript.adversary.name;
			fourth = adversary3Script.adversary.name;
			GrantVirtualCurrency();

			//medal = "Parabéns você ganhou medalha de ouro e "+prizecoins +" moedas!";
		}
		else if ((p==2)) {
			prizecoins = 50;
			first = adversary3Script.adversary.name;
			second = "Terezinha Guilhermina e \nRafael Lazarini";
			third = adversaryScript.adversary.name;
			fourth = adversary2Script.adversary.name;
			GrantVirtualCurrency();
			//medal = "Parabéns você ganhou medalha de prata e "+prizecoins +" moedas!";
		}
		else if ((p==3)) {
			prizecoins = 25;
			first = adversary3Script.adversary.name;
			second = adversary2Script.adversary.name;
			third = "Terezinha Guilhermina e \nRafael Lazarini";
			fourth = adversaryScript.adversary.name;
			GrantVirtualCurrency();
			//medal = "Parabéns você ganhou medalha de bronze e "+prizecoins +" moedas!";;
		}
		else if ((p==4)) {
			//medal = "Não foi dessa vez! Tente mais vezes e conquiste medalhas!";
			first = adversary3Script.adversary.name;
			second = adversary2Script.adversary.name;
			third = adversaryScript.adversary.name;
			fourth = "Terezinha Guilhermina e \nRafael Lazarini";
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
			
			adversary2Script.adversary.featuresAdversary (Random.Range (1,9));
			adversary3Script.adversary.featuresAdversary (Random.Range (1,9));
			
		}
		
		
	}

	void OnError(PlayFabError error)
	{
		Debug.Log("Error: " + error.ErrorMessage);
	}
	void OnGrantVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log("Currency granted!");
		playfabManager.GetVirtualCurrencies();
	}
	public void GrantVirtualCurrency()
	{
		var request = new AddUserVirtualCurrencyRequest
		{
			VirtualCurrency = "PJ",
			Amount = prizecoins
		};
		PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantVirtualCurrencySuccess, OnError);
	}
}