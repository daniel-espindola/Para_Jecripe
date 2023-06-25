using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimminMainPlayerV4 : MonoBehaviour
{
	private static SwimminMainPlayerV4 instance;

	public SystemMainSwimmingV4 SwimmingV4;

	public bool armStrokesOK;
	public GameObject player;
	Timer at; //armStrokesTimer
	bool paused;
	public bool normalTutorial;
	string lastSide;
	Vector3 movement;
	Vector3 maxvel;


	Vector3 rotationVector;

	SwimmingSounds sounds;

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{

		player = GameObject.Find("Player");
		maxvel = new Vector3(0.0f, 0.0f, 3);
		movement = new Vector3(0, 0, 11f);
		lastSide = "left";

		rotationVector.x = -90;
		rotationVector.y = 180;
		rotationVector.z = 0;


		at = GameObject.Find("ArmstrokeTimer").GetComponent<Timer>();
		sounds = GameObject.Find("Soundscript").GetComponent<SwimmingSounds>();


	}


	void Update()
	{
		if (!normalTutorial)
		{
			paused = SwimmingV4.isPaused;
		}

		if (SwimmingV4.isPaused == false)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				armStrokesOK = true;
				player.transform.rotation = Quaternion.Euler(rotationVector);
			}
		}
		if (!paused)
		{
			if (armStrokesOK)
			{
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					ArmStroke("left", "leftArmStrokeTrigger");
				}
				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					ArmStroke("right", "rightArmStrokeTrigger");
				}

			}
		}
		if (normalTutorial == true)
		{
			if (armStrokesOK)
			{
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					ArmStroke("left", "leftArmStrokeTrigger");
				}
				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					ArmStroke("right", "rightArmStrokeTrigger");
				}

			}
		}

		//Debug.Log(armStrokesOK);
	}

	void ArmStroke(string thisSide, string sideTrigger)
	{
		if (player.GetComponent<Rigidbody>().velocity.z < maxvel.z
			&& at.time <= 2f
			&& lastSide != thisSide)
		{

			player.GetComponent<Animator>().SetTrigger(sideTrigger);
			MoveForward();
			at.SetTimer();
			lastSide = thisSide;
			sounds.ArmStrokeSound();
			//rotationVector = player.transform.eulerAngles;

			player.transform.rotation = Quaternion.Euler(rotationVector);

		}
		else
		{
			at.ResetTimer();
		}

	}

	void MoveForward()
	{
		player.GetComponent<Rigidbody>().AddForce(movement * 13);
		//Debug.Log(player.GetComponent<Rigidbody>().velocity);
	}
}
