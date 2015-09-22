﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject player;
	private PlayerController pC;
	public GameObject pHitArea;
	public GameObject playerTarget;
	
	public GameObject enemy;
	private EnemyController eC;
	public GameObject eHitArea;
	public GameObject enemyTarget;
	
	public GameObject ball;
	private Rigidbody ballRB;
	
	public GameObject Fade;
	private Renderer fadeRenderer;
	private Color fadeColor;
	private bool fadeIn, fadeOut;
	private float fadeTime = 0f;
	
	private int bounces;
	
	private bool inGame;
	private int serve; //1 = player, -1 = enemy;
	
	private int servingSide=1; //-1 left, 1 Right
	
	public Text pS, pG, pP1, pP2;
	public Text eS, eG, eP1, eP2;
	
	public GameObject DisplayScore;
	public Text displayScore;
	public Text scoreMessage;		 
	
	private BallController bC;
	
	public bool playerTurn;
	
	private string[] score;
	private string[] score2;
	private int playerScore;
	private int enemyScore;
	private int playerGameCount;
	private int enemyGameCount;
	private int playerSetCount;
	private int enemySetCount;


	void Start () {
		ballRB = ball.GetComponent<Rigidbody> ();

		DisplayScore.SetActive (false);
		fadeIn = false;
		fadeOut = false;
		fadeRenderer = Fade.GetComponent<Renderer>();
		fadeColor = fadeRenderer.material.color;
		fadeColor.a = 0;
		fadeRenderer.material.color = fadeColor;
		
		pC = player.GetComponent<PlayerController> ();
		eC = enemy.GetComponent<EnemyController>();
		bC = ball.GetComponent<BallController> ();
		
		
		score = new string[]{"0","1","3","4","A","0"};	
		score2 = new string[] {"0","5","0","0","D","0"};
		
		playerScore = 0;
		enemyScore = 0;
		playerGameCount = 0;
		enemyGameCount = 0;
		playerSetCount = 0;
		enemySetCount = 0;
		serve = 1;

		pS.text = playerSetCount+"";
		pG.text = playerGameCount + "";
		pP1.text = score [playerScore] + "";
		pP2.text = score2 [playerScore] + "";
		eS.text = enemySetCount+"";
		eG.text = enemyGameCount + "";
		eP1.text = score [enemyScore] + "";
		eP2.text = score2 [enemyScore] + "";

		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeOut == true){
			fadeTime += Time.deltaTime;
			if (fadeTime> 2 && fadeTime<2.5){
				fadeColor.a += 0.06f;
				fadeRenderer.material.color = fadeColor;
			}
			else if(fadeTime>=2.5f){
				DisplayScore.SetActive(false);
				fadeTime = 0;
				fadeOut = false;
				fadeIn = true;
				StartGame();
			}
		}
		if(fadeIn == true){
			fadeTime += Time.deltaTime;
			if (fadeTime>0.5 && fadeTime<1){
				fadeColor.a -= 0.07f;
				if(fadeColor.a <0){
					fadeColor.a =0;
				}
				fadeRenderer.material.color = fadeColor;
			}
			else if(fadeTime>=1){
				fadeTime = 0;
				fadeIn = false;
			}
		}
	}
	
	public void AddBounce(){
		bounces ++;
		if(bounces ==3){
			if(playerTurn == true){
				SetScore(-1);
			}
			else{
				SetScore(1);
			}
		}
	}
	public void ResetBounce(){
		bounces = 0;
	}
	private void SetScore(int t){		
		if(inGame == true){
			if(t == -1){
				enemyScore++;
				scoreMessage.text = "Ponto Adversario";
				ShowPoints();
			}
			else if(t== 1){
				playerScore++;
				scoreMessage.text = "Ponto Player";
				ShowPoints();
			}
			if (playerScore ==4 && enemyScore <3){
				scoreMessage.text = "Game Player";
				playerGameCount ++;
				ShowGameScore();
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
			else if (enemyScore ==4 && playerScore <3){
				scoreMessage.text = "Game Adversario";
				enemyGameCount ++;
				ShowGameScore();
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
			else if (enemyScore == 4 && playerScore == 4){
				scoreMessage.text = "Deuce";
				playerScore = 3;
				enemyScore = 3;
				ShowPoints();
			}
			else if (playerScore ==5){
				scoreMessage.text = "Game Player";
				playerGameCount ++;
				ShowGameScore();
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
			else if(enemyScore == 5){
				scoreMessage.text = "Game Adversario";
				enemyGameCount ++;
				ShowGameScore();
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
			if(playerGameCount == 2){
				scoreMessage.text = "Set Player";
				playerSetCount ++;
				ShowSetScore();
				playerGameCount = 0;
				enemyGameCount = 0;
			}
			else if(enemyGameCount == 2){
				scoreMessage.text = "Set Adversario";
				enemySetCount ++;
				ShowSetScore();
				playerGameCount = 0;
				enemyGameCount = 0;
			}
		}
		DisplayScore.SetActive(true);
		inGame = false;
		pS.text = playerSetCount+"";
		pG.text = playerGameCount + "";
		pP1.text = score [playerScore] + "";
		pP2.text = score2 [playerScore] + "";
		eS.text = enemySetCount+"";
		eG.text = enemyGameCount + "";
		eP1.text = score [enemyScore] + "";
		eP2.text = score2 [enemyScore] + "";
		fadeOut = true;
	}
	
	private void StartGame(){
		inGame = true;
		
		Vector3 pStartPosition = new Vector3(2f*servingSide, 0.519f, -12.47f);
		player.transform.position = pStartPosition;
		player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
		
		Vector3 eStartPosition = new Vector3(-servingSide*2f, 0.519f, 12.47f);
		enemy.transform.position = eStartPosition;
		enemy.transform.rotation = Quaternion.Euler(0f,0f,0f);

		ballRB.velocity = Vector3.zero;
		ballRB.useGravity = false;
		bC.isServing = true;
		
		if(serve==1){
			pC.isServing = true;
			pHitArea.SetActive (false);
			Vector3 serveTarget = new Vector3(-servingSide * 2.5f, 0f, 3.5f);
			playerTarget.transform.position = serveTarget;
			playerTurn = true;
		}
		else if (serve == -1){		
			eC.isServing = true;
			eHitArea.SetActive (false);
			Vector3 serveTarget = new Vector3(servingSide * 2.5f, 0f, -3.5f);
			enemyTarget.transform.position = serveTarget;
			playerTurn = false;
		}

		if(servingSide == 1){
			ball.transform.position = new Vector3 (serve*2.127f, 1.6f, -serve*12.01f);
		}
		else{
			ball.transform.position = new Vector3 (-serve*1.87f, 1.6f, -serve*12.01f);
		}

		servingSide *= -1;
	}
	
	public void PlayerSideHit(){
		if(playerTurn == false){
			SetScore(-1);
		}
		else{
			AddBounce();
		}
	}
	public void EnemySideHit(){
		if(playerTurn==true){
			SetScore(1);
		}
		else{
			AddBounce();
		}
	}
	
	public void OutHit(){
		if (bounces >=1){
			AddBounce();
		}
		else{
			if(playerTurn == true){
				SetScore(1);
			}
			else{
				SetScore(-1);
			}
		}
	}
	public void WallHit(){
		if(bounces >=1){
			if(playerTurn == true){
				SetScore(-1);
			}
			else{
				SetScore(1);
			}
		}
	}
	
	private void ShowPoints(){
		if(serve == 1){
			displayScore.text = score[playerScore]+score2[playerScore] +" - "+ score[enemyScore]+score2[enemyScore];
		}
		else{
			displayScore.text = score[enemyScore]+score2[enemyScore] +" - "+ score[playerScore]+score2[playerScore];
		}	
		if (playerScore == 3 && enemyScore == 3) {
			scoreMessage.text = "Deuce";
		}
	}
	
	private void ShowGameScore(){
		displayScore.text = playerGameCount + " - " + enemyGameCount;
	}
	
	private void ShowSetScore(){
		displayScore.text = playerSetCount+" - "+enemySetCount;
	}
	
	public void BackToMenu ()
	{
		Application.LoadLevel ("PlayTennis");
	}
}

