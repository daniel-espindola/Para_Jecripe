﻿using UnityEngine;
using System.Collections;

public class SwimmingTutorial_Player : MonoBehaviour {

	
	public bool armStrokesOK;
	GameObject player;
	Timer at; //armStrokesTimer
	
	string lastSide;
	Vector3 movement;
	Vector3 maxvel;
	
	
	
	
	void Start () {
	
		player = GameObject.Find ("Player");
		maxvel = new Vector3 (0.0f, 0.0f, 3);
		movement = new Vector3 (0, 0, 11f);
		lastSide = "left";
		
		at = GameObject.Find ("ArmstrokeTimer").GetComponent<Timer>();
		
		
	}
	
	
	void Update () {
		
		if(armStrokesOK){
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				ArmStroke ("left", "leftArmStrokeTrigger");
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				ArmStroke ("right", "rightArmStrokeTrigger");
			}
			
		}
	
	}
	
	void ArmStroke (string thisSide, string sideTrigger)
	{
		if (player.GetComponent<Rigidbody> ().velocity.z < maxvel.z 
		    && at.time <= 2f
		    && lastSide != thisSide) {
			
			player.GetComponent<Animator> ().SetTrigger (sideTrigger);
			MoveForward ();
			at.SetTimer ();
			lastSide = thisSide;
			
		} else {
			at.ResetTimer ();
		}
		
	}
	
	void MoveForward ()
	{
		player.GetComponent<Rigidbody> ().AddForce (movement * 13);
		Debug.Log (player.GetComponent<Rigidbody> ().velocity);
	}
}