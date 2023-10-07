using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoTopDown : MonoBehaviour
{
    public float velocidade = 5.0f; // Velocidade de movimenta��o

    private Rigidbody rb;

    void Start()
    {
        // Obt�m o componente Rigidbody do objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obt�m a entrada do teclado
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        // Calcula o vetor de movimento
        Vector3 movimento = new Vector3(movimentoHorizontal, 0.0f, movimentoVertical);

        // Normaliza o vetor de movimento para evitar movimentos diagonais mais r�pidos
        if (movimento.magnitude > 1f)
        {
            movimento.Normalize();
        }

        // Aplica a velocidade ao Rigidbody
        rb.velocity = movimento * velocidade;

        // Verifica se h� movimento e ajusta a rota��o
        if (movimento != Vector3.zero)
        {
            Quaternion novaRotacao = Quaternion.LookRotation(movimento);
            rb.MoveRotation(novaRotacao);
        }
    }
}
