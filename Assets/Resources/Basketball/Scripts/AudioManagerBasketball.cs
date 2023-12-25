using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBasketball : MonoBehaviour
{
    public AudioSource audioSource1; // Refer�ncia ao AudioSource do �udio 1
    public AudioSource audioSource2; // Refer�ncia ao AudioSource do �udio 2
    public AudioSource audioSource3; // Refer�ncia ao AudioSource do �udio 4
    private static AudioManagerBasketball audioManagerBasketball;
    private void Awake()
    {
        audioManagerBasketball = this;
    }
    void Start()
    {
        // Chama a fun��o para reproduzir o �udio 1 por 5 segundos ap�s um atraso de 1 segundo
        Invoke("PlayAudio1", 3.5f);
    }

    public void PlayAudio1()
    {
        if (audioSource1 != null)
        {
            audioSource1.Play();
            Invoke("StopAudio1", 5f); // Chama a fun��o para parar o �udio 1 ap�s 5 segundos
        }
    }

    public void PlayAudio2()
    {
        if (audioSource2 != null)
        {
            audioSource2.Play();
            Invoke("StopAudio2", 3.5f); // Chama a fun��o para parar o �udio 2 ap�s 3.5 segundos
        }
    }

    public void PlayAudio3()
    {
        if (audioSource3 != null)
        {
            audioSource3.Play();
            Invoke("StopAudio4", 3.5f); // Chama a fun��o para parar o �udio 3 ap�s 3.5 segundos
        }
    }

    void StopAudio1()
    {
        if (audioSource1 != null)
        {
            audioSource1.Stop();
        }
    }

    void StopAudio2()
    {
        if (audioSource2 != null)
        {
            audioSource2.Stop();
        }
    }

    void StopAudio4()
    {
        if (audioSource3 != null)
        {
            audioSource3.Stop();
        }
    }
}
