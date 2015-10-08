﻿/// <summary>
/// Question on button.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionOnButton : MonoBehaviour 
{

	public string answer;

	private Button button;
	private int addp;


	private Quiz quiz;
	private QuizButtons quizButtons;
	private QuizSounds sounds;


	void Start()
	{
		sounds = GameObject.Find ("Sounds").GetComponent<QuizSounds>();
		quiz = GameObject.Find("QuizScript").GetComponent<Quiz>();
		quizButtons = GameObject.Find("ButtonScript").GetComponent<QuizButtons>();
	}


	void Update()
	{
		answer = GetComponentInChildren<Text>().text;
		
	}


	/// <summary>
	/// Chosens the answer and changes the scores and the countdown acording with the correct answer.
	/// </summary>
	public void ChosenAnswer()
	{	
		if(answer == quiz.Correct)
		{
			addp = 1000;
			sounds.PlaySound(sounds.rightSound);
			
		} 
		else 
		{
			addp = 0;
			quiz.timeLeft = quiz.timeLeft - Time.deltaTime - 10;
			sounds.PlaySound (sounds.wrongSound);
		}
		quiz.setRand = false;
		quizButtons.ChangeScore (addp);
		
	}

}
