using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoTopDown : MonoBehaviour
{
    public float velocidade = 5.0f; // Velocidade de movimentação

    private Rigidbody rb;

    void Start()
    {
        // Obtém o componente Rigidbody do objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtém a entrada do teclado
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        // Calcula o vetor de movimento
        Vector3 movimento = new Vector3(movimentoHorizontal, 0.0f, movimentoVertical);

        // Normaliza o vetor de movimento para evitar movimentos diagonais mais rápidos
        if (movimento.magnitude > 1f)
        {
            movimento.Normalize();
        }

        // Aplica a velocidade ao Rigidbody
        rb.velocity = movimento * velocidade;

        // Verifica se há movimento e ajusta a rotação
        if (movimento != Vector3.zero)
        {
            Quaternion novaRotacao = Quaternion.LookRotation(movimento);
            rb.MoveRotation(novaRotacao);
        }
    }
}
