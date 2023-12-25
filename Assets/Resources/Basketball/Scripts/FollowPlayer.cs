using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Refer�ncia ao transform do jogador para seguir

    public Vector3 offset = new Vector3(0, 5, -5); // Ajuste a posi��o da c�mera em rela��o ao jogador

    public float smoothness = 2.0f; // Ajuste a suavidade de movimento da c�mera

    public float minZ = -10.0f; // Valor m�nimo no eixo Z

    public float maxZ = 10.0f; // Valor m�ximo no eixo Z

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula a posi��o desejada da c�mera
            Vector3 desiredPosition = player.position + offset;

            // Limita a posi��o no eixo Z
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, minZ, maxZ);

            // Interpola suavemente a posi��o atual da c�mera em dire��o � posi��o desejada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);
        }
    }
}
