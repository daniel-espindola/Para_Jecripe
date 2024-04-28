using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SystemMainNPC : MonoBehaviour
{
    //PUBLIC
    public ScoreManagerBasketball scoreManager;
    public ThrowObject throwObject;
    public Transform holdPosition; // A posi��o onde a bola ser� mantida quando estiver segurando-a.
    public MovimentacaoNavMesh movimentacaoNavMesh;
    public float pickupDistance = 2f; // A dist�ncia m�xima para pegar a bola. 
    public bool podeGerar;
    public bool getBall;
    public bool inGame;
    public bool podePegar;
    public string objectName;

    //PRIVATE
    private static SystemMainNPC systemMainNPC;
    private Animator animator; // Refer�ncia ao Animator.
    private GameObject heldObject; // Refer�ncia para a bola que o jogador est� segurando.
    private bool isNearBall = false;//Para indicar proximidade com a bola.
    private bool hasHitAnimationPlayed = false; // Para controlar se a anima��o "Hit" j� foi reproduzida.
    float randomValue;
    GameObject obj;

    private void Awake()
    {
        systemMainNPC = this;
        obj = GameObject.Find(objectName);
        StartCoroutine(GetBaloObj());

    }
    private void Start()
    {
        StartCoroutine(GetBaloObj());
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Restaurar o status da anima��o "Hit" para que possa ser acionada novamente.
        hasHitAnimationPlayed = false;

        if(podeGerar == true)
        {
            //Pegar bola?
            GenerateRandomNumber();
        }

        // Atualizar a vari�vel isNearBall com base na proximidade da bola.
        UpdateNearBallStatus();

        // Detecta se o jogador est� perto o suficiente da bola para peg�-la.
        if(podePegar == true)
        {
            if (randomValue >= 0.85f && heldObject == null)
            {
                TryPickupBall();
                if (isNearBall == true /*&& throwObject.observe == true*/)
                {
                    StartCoroutine(JogarBola());
                    StartCoroutine(AntGlitch());
                }
            }
        }
        
    }
    void TryPickupBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ball"))
            {
                podePegar = false;
                podeGerar = false;
                PickupBall(collider.gameObject);
                break;
            }
        }
    }
    void PickupBall(GameObject ball)
    {
        ball.GetComponent<Rigidbody>().isKinematic = true; // Impede a bola de cair devido � f�sica.
        ball.transform.position = holdPosition.position;
        ball.transform.parent = transform;
        heldObject = ball;

        //Pegar bolar
        getBall = true;

        // Execute a anima��o de Hit.
        animator.SetTrigger("Hit");

        // Pare de correr definindo a velocidade para zero.
        animator.SetBool("IsRunning", false);

        // Defina a flag para evitar que a anima��o seja chamada novamente.
        hasHitAnimationPlayed = true;

        scoreManager.enemy = true;
        ball.transform.position = holdPosition.position;
    }
    void ReleaseBall()
    {
        podePegar = false;
        //heldObject.GetComponent<Rigidbody>().isKinematic = false; // Restaura a f�sica da bola.
        heldObject.transform.parent = null;
        heldObject = null;

        // Execute a anima��o de Hit.
        animator.SetTrigger("Arremesso");

        // Pare de correr definindo a velocidade para zero.
        animator.SetBool("IsRunning", false);

        StartCoroutine(OffEnemy());
        if(inGame==true) {
            movimentacaoNavMesh.TargetBall();
        }
        
    }
    float GenerateRandomNumber()
    {
        // Gera um n�mero aleat�rio entre 0 (inclusive) e 1 (exclusivo).
        randomValue = Random.Range(0.0f, 1f);

        return randomValue;
    }
    IEnumerator JogarBola()
    {
        // Aguarda por 2 segundos.
        yield return new WaitForSeconds(2f);
        ReleaseBall();
        throwObject.IniciarMovimento();
        yield return new WaitForSeconds(5f);
        podeGerar = true;
        
    }
    IEnumerator OffEnemy()
    {
        // Aguarda por 2 segundos.
        yield return new WaitForSeconds(3f);
        scoreManager.enemy = false;
        if(heldObject != null) {
            heldObject.transform.parent = null;
        }
        
        
    }
    IEnumerator AntGlitch()
    {
        yield return new WaitForSeconds(15f);
        podePegar = true;

    }
    void UpdateNearBallStatus()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupDistance);

        // Verificar se h� uma bola dentro da esfera de colis�o.
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ball"))
            {
                if(inGame == true)
                {
                    movimentacaoNavMesh.Target();
                }               
                isNearBall = true;
                return; // Se encontrou uma bola, n�o precisa verificar mais.
            }
        }

        // Se n�o encontrou uma bola, define isNearBall como false.
        isNearBall = false;
    }
    IEnumerator GetBaloObj()
    {
        yield return new WaitForSeconds(1f);
        throwObject = obj.GetComponent<ThrowObject>();       
    }
}

