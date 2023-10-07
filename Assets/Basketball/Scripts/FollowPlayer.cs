using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Refer�ncia ao transform do jogador para seguir

    public Vector3 offset = new Vector3(0, 5, -5); // Ajuste a posi��o da c�mera em rela��o ao jogador

    public float smoothness = 2.0f; // Ajuste a suavidade de movimento da c�mera

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula a posi��o desejada da c�mera
            Vector3 desiredPosition = player.position + offset;

            // Interpola suavemente a posi��o atual da c�mera em dire��o � posi��o desejada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);
        }
    }
}
