/*
    INSTITUTO TECNOLÓGICO DE COSTA RICA CTLSC
    ESCUELA DE INGENIERÍA EN COMPUTACIÓN
    INTRODUCCIÓNA A LA REALIDAD VIRTUAL - I SEMESTRE 2024
    PROYECTO: ENSEÑA A NIÑOS

    INFORMACIÓN DEL CÓDIGO
    CLASE PARA DETECTAR EL INGRESO DE UN OBJETO DENTRO DEL COLISIONADOR DE MALLA DENTRO DEL OBJETO QUE SE LE ASIGNE EL SCRIPT
    E IDENTIFICAR SI SON DEL MISMO COLOR REPRODUCIENDO UN SONIDO SEGÚN LA IGUALDAD DE COLORES DE LOS OBJETOS
    AUTOR: JUAN BAUTISTA NÚÑEZ PARRALES
    ÚLTIMA MODIFICACIÓN: JUAN BAUTISTA NÚÑEZ PARRALES - FECHA DE MODIFICACIÓN: 10/06/2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSphere : MonoBehaviour
{
    public Color referenceColor;  // Color de referencia
    public AudioClip sameColorClip;  // Clip de audio para el mismo color
    public AudioClip differentColorClip;  // Clip de audio para diferente color
    public AudioClip colorNameClip; // Clip de audio para color identificado

    private int sphereCount = 0;  // Contador de esferas
    private AudioSource audioSource;  // Referencia al componente AudioSource
    private HashSet<GameObject> enteredObjects = new HashSet<GameObject>();  // Almacena objetos que ya han entrado

    // Método de inicio
    private void Start()
    {
        // Referencia al AudioSource en el mismo objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si no hay un componente AudioSource, agrega uno
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Método para reproducir sonidos si un objeto entra al objeto actual y compara el color de sus texturas
    private void OnTriggerEnter(Collider other)
    {   
        if (!enteredObjects.Contains(other.gameObject))
        {
            enteredObjects.Add(other.gameObject);

            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color objectColor = renderer.material.color;  // Obtiene el color del objeto que entra
                if (ColorsAreSimilar(referenceColor, objectColor))
                {
                    sphereCount++;
                    Debug.Log("Esferas en la canasta: " + sphereCount);
                    Debug.Log("Un objeto del mismo color ha entrado en la canasta: " + other.gameObject.name);

                    // Reproduce el sonido para el mismo color y luego el nombre del color
                    StartCoroutine(PlaySameColorAndNameClips());
                }
                else
                {
                    Debug.Log("Un objeto de diferente color ha entrado en la canasta: " + other.gameObject.name);

                    // Reproduce el sonido para diferente color
                    PlaySound(differentColorClip);
                }
            }
            else
            {
                Debug.Log("El objeto entrante no tiene un componente Renderer.");
            }
        }
    }

    // Método para reproducir el sonido cuando el otro termine
    private IEnumerator PlaySameColorAndNameClips()
    {
        // Reproduce el sonido para el mismo color
        PlaySound(sameColorClip);

        // Espera a que termine el mismoColorClip
        yield return new WaitForSeconds(sameColorClip.length);

        // Luego reproduce el sonido del nombre del color
        PlaySound(colorNameClip);
    }

    // Método para reproducir el sonido
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Método para comparar colores con cierto margen de tolerancia para diferencias menores
    private bool ColorsAreSimilar(Color a, Color b, float tolerance = 0.1f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance && Mathf.Abs(a.g - b.g) < tolerance && Mathf.Abs(a.b - b.b) < tolerance;
    }
}
