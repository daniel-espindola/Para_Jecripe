using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GetBall : MonoBehaviour
{
    //PUBLIC
    public bool ingame;
    public float pickupDistance = 2f; // A dist�ncia m�xima para pegar a bola.
    public Transform holdPosition; // A posi��o onde a bola ser� mantida quando estiver segurando-a. 
    public bool getBall;
    public SetArremesso setArremesso;
    public ThrowObject throwObject;
    public SystemMainNPC systemMainNPC;
    public BasketBallTutorial basketBallTutorial;
    public MovimentacaoNavMesh movimentacaoNavMesh;
    public bool onTutorial;
    public string objectName;
    public bool onArremesso;
    //PRIVATE
    private static GetBall getBallx16;
    private bool hasHitAnimationPlayed = false; // Para controlar se a anima��o "Hit" j� foi reproduzida.
    private Animator animator; // Refer�ncia ao Animator.
    private GameObject heldObject; // Refer�ncia para a bola que o jogador est� segurando.
    GameObject obj;
    bool dd;
    private void Awake()
    {
        getBallx16 = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ToggleBoolEveryTenSecondsCoroutine());
        StartCoroutine(GetBaloObj());
         obj = GameObject.Find(objectName);
        StartCoroutine(DebugFix());
    }
    void Update()
    {
        // Restaurar o status da anima��o "Hit" para que possa ser acionada novamente.
        hasHitAnimationPlayed = false;

        // Detecta se o jogador est� perto o suficiente da bola para peg�-la.
        if (SimpleInput.GetKeyDown(KeyCode.G) /*&& heldObject == null*/)
        {
            TryPickupBall();
        }
        // Solta a bola quando pressionada a tecla "Space".
#if MOBILE_INPUT
        else if (SimpleInput.GetKeyDown(KeyCode.Space) && heldObject != null && !throwObject.podeMover && setArremesso.arremesso == true)
        {
            ReleaseBall();
            throwObject.IniciarMovimento();
            if (ingame == true)
            {
                movimentacaoNavMesh.TargetBall();
            }
        }
#else
        if (onArremesso == true)
        {
            if (SimpleInput.GetKeyDown(KeyCode.Space) && heldObject != null && !throwObject.podeMover && setArremesso.arremesso == true)
            {
                ReleaseBall();
                throwObject.IniciarMovimento();
                if (ingame == true)
                {
                movimentacaoNavMesh.TargetBall();
                }

            }
        }
       
#endif

        //Debug.Log(onArremesso);
    }

    void TryPickupBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ball"))
            {
                if(onTutorial == true) {
                    basketBallTutorial.startPart02 = false;
                    basketBallTutorial.startPart03 = true;
                    onTutorial = false;
                }
                movimentacaoNavMesh.TargetPlayer();
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
        animator.SetFloat("Speed", 0);

        // Defina a flag para evitar que a anima��o seja chamada novamente.
        hasHitAnimationPlayed = true;
    }

    void ReleaseBall()
    {
        //heldObject.GetComponent<Rigidbody>().isKinematic = false; // Restaura a f�sica da bola.
        heldObject.transform.parent = null;
        heldObject = null;

        // Execute a anima��o de Hit.
        animator.SetTrigger("Arremesso");

        // Pare de correr definindo a velocidade para zero.
        animator.SetFloat("Speed", 0);

    }
    IEnumerator ToggleBoolEveryTenSecondsCoroutine()
    {
        while (true)
        {
            // Espera por 10 segundos.
            yield return new WaitForSeconds(15f);

            // Alterna a bool entre true e false.
            systemMainNPC.podeGerar = !systemMainNPC.podeGerar;

        }
    }
    IEnumerator GetBaloObj()
    {
        yield return new WaitForSeconds(.5f);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        throwObject = obj.GetComponent<ThrowObject>();
    }
    IEnumerator DebugFix()
    {
        yield return new WaitForSeconds(5.5f);
        dd = true;

    }
}