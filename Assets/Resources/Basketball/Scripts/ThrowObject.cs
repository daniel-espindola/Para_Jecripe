using Blobcreate.ProjectileToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public bool onTutorial;
    public BasketBallTutorial basketBallTutorial;
    public ScoreManagerBasketball scoreManager;
    public Transform pontoB; // Objeto ou posi��o para B
    public Transform pontoC; // Objeto ou posi��o para C
    public Transform pontoD; // Objeto ou posi��o para D
    public Transform pontoE; // Objeto ou posi��o para E
    public Transform pontoF; // Objeto ou posi��o para F
    public Transform pontoG; // Objeto ou posi��o para G
    public Transform pontoH; // Objeto ou posi��o para H
    public Transform pontoI; // Objeto ou posi��o para I
    public Transform pontoJ; // Objeto ou posi��o para J
    public Transform pontoK; // Objeto ou posi��o para K
    public Transform pontoL; // Objeto ou posi��o para L
    public Transform pontoM; // Objeto ou posi��o para M
    public Transform pontoN; // Objeto ou posi��o para N
    public Transform pontoO; // Objeto ou posi��o para O
    public Transform pontoP; // Objeto ou posi��o para P
    public Rigidbody rb; // Rigidbody do objeto
    [Header("Audio Manager")]
    public AudioManagerBasketball audioManagerBasketball;

    private static ThrowObject throwObject;
    private Transform destino; // Pr�ximo destino
    public bool podeMover = false;
    public ScoreManagerBasketball sM;
    public bool observe;

    private void Awake()
    {
        throwObject = this;       
    }

    private void Start()
    {
        EscolherProximoDestino();
    }

    private void Update()
    {
        
        if (podeMover)
        {
            // Move o objeto em dire��o ao destino escolhido
            float velocidade = 5.0f; // Velocidade de movimento (ajuste conforme necess�rio)
            transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidade * Time.deltaTime);

            // Verifica se o objeto chegou ao destino
            if (Vector3.Distance(transform.position, destino.position) < 0.01f)
            {
                ChegouAoDestino();
            }
            
        }

        
    }
    
    private void ChegouAoDestino()
    {
        // Desativa o Rigidbody kinematic quando o objeto chegar ao destino
        rb.isKinematic = false;

        // O objeto j� chegou ao destino e n�o se move mais
        podeMover = false;

        // Verifique se o destino atual � pontoB e chame a fun��o GrantScore
        if (destino == pontoB)
        {
            GrantScore();
        }

        //Proximo destino
        EscolherProximoDestino();
    }

    public void IniciarMovimento()
    {
        // Ativa o movimento do objeto
        podeMover = true;
    }

    public void EscolherProximoDestino()
    {
        // Gera um n�mero aleat�rio para escolher o pr�ximo destino (B, C ou D)
        int randomDestino = Random.Range(1, 15); // N�meros de 1 a 8

        switch (randomDestino)
        {
            case 1:
                destino = pontoB;
                break;
            case 2:
                destino = pontoC;
                break;
            case 3:
                destino = pontoD;
                break;
            case 4:
                destino = pontoE;
                break;
            case 5:
                destino = pontoF;
                break;
            case 6:
                destino = pontoG;
                break;
            case 7:
                destino = pontoH;
                break;
            case 8:
                destino = pontoI;
                break;
            case 9:
                destino = pontoJ;
                break;
            case 10:
                destino = pontoK;
                break;
            case 11:
                destino = pontoL;
                break;
            case 12:
                destino = pontoM;
                break;
            case 13:
                destino = pontoN;
                break;
            case 14:
                destino = pontoO;
                break;
            case 15:
                destino = pontoP;
                break;
        }
    }
    private void GrantScore()
    {
        Debug.Log("Score granted for reaching point B!");
        audioManagerBasketball.PlayAudio2();
        if (scoreManager.enemy==false) { sM.score++; }
        if (scoreManager.enemy == true) { sM.scoreEnemy++; }
        if (onTutorial)
        {
            basketBallTutorial.startPart03 = false;
            basketBallTutorial.startPart04 = true;
        }
        
    }
    
}


