using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballAudio : MonoBehaviour
{
    public AudioClip audioInicial; // �udio no in�cio da gameplay
    public AudioClip sfxPontuacao; // SFX para quando h� pontua��o
    public AudioClip audioDeFundo; // �udio de fundo ap�s o �udio inicial
    private AudioSource audioSource;
    private static BasketballAudio basketballAudio;

    private void Awake()
    {
        basketballAudio = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Toca o �udio inicial no in�cio da gameplay
        if (audioInicial != null)
        {
            audioSource.clip = audioInicial;
            audioSource.Play();
        }
    }

    // Fun��o para tocar o SFX de pontua��o
    public void TocarSFXPontuacao()
    {
        if (sfxPontuacao != null)
        {
            audioSource.PlayOneShot(sfxPontuacao);
        }
    }

    void Update()
    {
        // Verifica se o �udio inicial terminou de tocar e inicia o �udio de fundo
        if (!audioSource.isPlaying && audioDeFundo != null)
        {
            audioSource.clip = audioDeFundo;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
