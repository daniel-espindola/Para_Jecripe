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

	public GameObject Fade;
	private Renderer fadeRenderer;
	private Color fadeColor;
	private bool fadeIn, fadeOut;
	private float fadeTime = 0f;

	private Rigidbody r;

	private GameObject bola;

	private int bounces;

	private bool inGame;
	private int serve; //1 = player, -1 = enemy;

	private int servingSide=1; //-1 left, 1 Right

	public Text scoreText; 

	private BallController bC;

	public bool playerTurn;

	private string[] score;
	private int playerScore;
	private int enemyScore;
	private int playerGameCount;
	private int enemyGameCount;

	void Start () {
		fadeIn = false;
		fadeOut = false;
		fadeRenderer = Fade.GetComponent<Renderer>();
		fadeColor = fadeRenderer.material.color;
		fadeColor.a = 0;
		fadeRenderer.material.color = fadeColor;

		pC = player.GetComponent<PlayerController> ();
		eC = enemy.GetComponent<EnemyController>();
		bC = ball.GetComponent<BallController> ();
		
		score = new string[]{"0", "15", "30", "40", "ADV", "0"};		
		
		playerScore = 0;
		enemyScore = 0;
		playerGameCount = 0;
		enemyGameCount = 0;
		serve = 1;
		scoreText.text = "Jogador:  " + score[0] + "   " + playerGameCount+ "\n" +
						 "Oponente:  " + score[0] + "   " + playerGameCount; 
		StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeOut == true){
			fadeTime += Time.deltaTime;
			if (fadeTime> 2 && fadeTime<2.5){
				fadeColor.a += 0.05f;
				fadeRenderer.material.color = fadeColor;
			}
			else if(fadeTime>=2.5){
				fadeTime = 0;
				fadeOut = false;
				fadeIn = true;
				StartGame();
			}
		}
		if(fadeIn == true){
			fadeTime += Time.deltaTime;
			if (fadeTime<0.5){
				fadeColor.a -= 0.05f;
				fadeRenderer.material.color = fadeColor;
			}
			else if(fadeTime>=0.5){
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
			}
			else if(t== 1){
				playerScore++;
			}
			if (playerScore ==4 && enemyScore <3){
				playerGameCount ++;
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
			else if (enemyScore ==4 && playerScore <3){
				enemyGameCount ++;
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
			else if (enemyScore == 4 && playerScore == 4){
				playerScore = 3;
				enemyScore = 3;
			}
			else if (playerScore ==5){
				playerGameCount ++;
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
			else if(enemyScore == 5){
				enemyGameCount ++;
				enemyScore = 0;
				playerScore = 0;
				serve *= -1;
			}
		}
		inGame = false;
		scoreText.text = "Jogador:  " + score[playerScore] + "   " + playerGameCount+ "\n" +
					     "Oponente:  " + score[enemyScore]  + "   " + enemyGameCount; 
		pC.enabled = false;
		fadeOut = true;
	}

	private void StartGame(){
		inGame = true;
		pC.enabled = true;

		Vector3 pStartPosition = new Vector3(2f*servingSide, 0.519f, -12.47f);
		player.transform.position = pStartPosition;
		player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
		
		Vector3 eStartPosition = new Vector3(-servingSide*2f, 0.519f, 12.47f);
		enemy.transform.position = eStartPosition;
		
		bC.estaSacando = true;
		if(servingSide == 1){
			ball.transform.position = new Vector3 (serve*2.127f, 1.6f, -serve*12.01f);
		}
		else{
			ball.transform.position = new Vector3 (-serve*1.87f, 1.6f, -serve*12.01f);
		}

		if(serve==1){
			pC.estaSacando = true;
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


	public void BackToMenu ()
	{
		Application.LoadLevel ("PlayTennis");
	}
}
