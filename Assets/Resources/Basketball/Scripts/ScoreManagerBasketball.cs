using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManagerBasketball : MonoBehaviour
{
    public AudioManagerBasketball audioManagerBasketball;
    public bool onTutorial;
    public BasketBallTutorial basketBallTutorial;
    public bool enemy;
    public Text textScore;
    public Text textScoreEnemy;
    public int score = 0; // Inicializa a pontua��o em zero
    public int scoreEnemy = 0; // Inicializa a pontua��o em zero
    private static ScoreManagerBasketball scoreManager;
    private void Awake()
    {
        scoreManager = this;
    }
    void Start()
    {
        score = 0; // Certifica-se de que a pontua��o seja zero no in�cio
    }
    private void Update()
    {
        if(onTutorial == false) {
            textScore.text = score.ToString();
            textScoreEnemy.text = scoreEnemy.ToString();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && enemy == false)
        {
            // Se a colis�o for com um objeto que tem a tag "Ball", aumente a pontua��o
            score++;
            audioManagerBasketball.PlayAudio2();
            if (onTutorial && score > 0)
            {
                basketBallTutorial.startPart03 = false;
                basketBallTutorial.startPart04 = true;

            }
            Debug.Log("Pontua��o: " + score); // Exibe a pontua��o no console (opcional)
        }
        if (collision.gameObject.CompareTag("Ball") && enemy == true && enemy == !false)
        {
            // Se a colis�o for com um objeto que tem a tag "Ball", aumente a pontua��o
            scoreEnemy++;
            Debug.Log("Pontua��o: " + score); // Exibe a pontua��o no console (opcional)
        }
    }
}
