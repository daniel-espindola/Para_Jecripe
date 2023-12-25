using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BasketballScore : MonoBehaviour
{
    public Text textPoint;
    // Vari�vel para armazenar a pontua��o
    private int pontuacao = 0;

    // M�todo chamado quando algo entra no trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger tem a tag "BolaDeBasquete"
        if (other.CompareTag("BolaDeBasquete"))
        {
            // Incrementa a pontua��o
            pontuacao++;

            // Exibe a pontua��o no console (voc� pode substituir isso por sua l�gica de exibi��o)
            Debug.Log("Pontua��o de Basquete: " + pontuacao);

            // Voc� pode adicionar aqui qualquer outra l�gica desejada relacionada � pontua��o de basquete
        }
    }
    private void Update()
    {
        UpdateTXT();
    }
    private void UpdateTXT()
    {
        textPoint.text=pontuacao.ToString();
    }
}

