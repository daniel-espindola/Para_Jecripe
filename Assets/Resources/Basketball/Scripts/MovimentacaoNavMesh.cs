using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentacaoNavMesh : MonoBehaviour
{
    public float distanciaAfastamento = 2f; // Dist�ncia desejada entre o agente e o jogador
    public float distanciaParar = 1.5f;
    public Animator animator; // Refer�ncia ao componente Animator
    public RotateObjectBasketBall rotateObjectBasketBallL;
    public RotateObjectBasketBall rotateObjectBasketBallR;
    private Transform alvo01; // O jogador que o agente deve seguir
    private NavMeshAgent agente; // Refer�ncia ao componente NavMeshAgent
    private string nameTag;
    private static MovimentacaoNavMesh movimentacaoNavMesh;
    private void Awake()
    {
        movimentacaoNavMesh = this;
    }
    void Start()
    {
        TargetBall();
        alvo01 = GameObject.FindWithTag(nameTag).transform; // Encontre o jogador com uma tag "Player" (voc� pode ajustar isso de acordo com o seu projeto)
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Certifique-se de que o componente Animator est� presente no GameObject
    }

    void Update()
    {
        if (alvo01 != null)
        {
            // Calcula a dire��o do agente para o jogador
            Vector3 direcaoParaJogador = alvo01.position - transform.position;

            // Normaliza a dire��o e multiplica pela dist�ncia de afastamento desejada
            Vector3 destinoAfastado = alvo01.position - direcaoParaJogador.normalized * distanciaAfastamento;

            // Configura o destino do agente para a posi��o afastada do jogador
            agente.SetDestination(destinoAfastado);

            // Verifica se o agente est� se movendo
            bool estaSeMovendo = agente.velocity.magnitude > 0.1f;

            // Atualiza o par�metro "IsRunning" na anima��o com base no movimento
            animator.SetBool("IsRunning", estaSeMovendo);

            // Verifica se o agente est� pr�ximo o suficiente do jogador para parar a anima��o
            if (agente.remainingDistance <= distanciaParar)
            {
                animator.SetBool("IsRunning", false);
            }
            // Verifica se o alvo � o "Target" e vira o agente para ele
            if (nameTag == "Target")
            {
                // Obt�m a dire��o para o alvo
                Vector3 directionToTarget = alvo01.position - transform.position;
                directionToTarget.y = 0f; // Garante que a rota��o seja apenas no plano horizontal

                // Rotaciona o agente para olhar na dire��o do alvo
                if (directionToTarget != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Ajuste a velocidade da rota��o conforme necess�rio
                }
            }
            if(estaSeMovendo == true) 
            {
                rotateObjectBasketBallL.rot = estaSeMovendo;
                rotateObjectBasketBallR.rot = estaSeMovendo;
            }
            if (estaSeMovendo == false)
            {
                rotateObjectBasketBallL.rot = estaSeMovendo;
                rotateObjectBasketBallR.rot = estaSeMovendo;
            }
        }

    }

    public void TargetPlayer()
    {
        nameTag = "Player";
    }
    public void TargetBall()
    {
        nameTag = "Ball";
    }
    public void Target()
    {
        nameTag = "Target";
    }
}