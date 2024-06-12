using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Oculus;

// Clase serializable para almacenar la asociación entre una palabra y video
[System.Serializable]
public class LetterVideoAssociation
{
    public string word;
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public VideoClip video;
}

public class PlayWordVideo : MonoBehaviour
{
    // Atributos -------------------------------------------------------------
    
    // Lista de asociaciones entre palabras y videos
    public List<LetterVideoAssociation> letterVideoAssociations;

    // Diccionario para acceso rápido a los videos por palabra
    private Dictionary<string, GameObject> wordToVideo;
    private List<GameObject> currentInstances; // Lista para almacenar los videos reproducidos
    private int currentWordIndex; // Índice de la palabra actual en la lista

    // Métodos ---------------------------------------------------------------
    
    // Método para reproducir el video actual de la lista
    void PlayCurrentVideo()
    {
        if (letterVideoAssociations.Count > 0)
        {
            var currentAssociation = letterVideoAssociations[currentWordIndex];
            if (currentAssociation.videoPlayer != null && currentAssociation.video != null)
            {
                currentAssociation.videoPlayer.clip = currentAssociation.video;
                currentAssociation.videoPlayer.Play();
            }
        }
    }

    // Método para reproducir el video siguiente de la lista
    void PlayNextVideo()
    {
        // Detener el video actual
        var currentAssociation = letterVideoAssociations[currentWordIndex];
        if (currentAssociation.videoPlayer != null)
        {
            currentAssociation.videoPlayer.Stop();
        }

        // Avanzar al siguiente índice de palabra
        currentWordIndex = (currentWordIndex + 1) % letterVideoAssociations.Count;

        // Reproducir el siguiente video
        PlayCurrentVideo();
    }

    // Método para asociar una palabra de una lista a un video
    void InitializeWordToVideo()
    {
        // Inicializar el diccionario
        wordToVideo = new Dictionary<string, GameObject>();

        // Asocia las palabras con los videos en el diccionario
        foreach (var association in letterVideoAssociations)
        {
            if (!string.IsNullOrEmpty(association.word))
            {
                string key = association.word;
                wordToVideo[key] = association.videoPlayer.gameObject;
                //Debug.Log(key.ToString());

                // Asignar el VideoClip al VideoPlayer
                if (association.videoPlayer != null && association.video != null)
                {
                    association.videoPlayer.clip = association.video;
                    currentInstances.Add(association.videoPlayer.gameObject);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Detectar si se ha presionado la tecla 'C'
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            // Inicializar la lista de instancias actuales
            currentInstances = new List<GameObject>();
            currentWordIndex = 0;

            InitializeWordToVideo();
            PlayCurrentVideo();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Detectar si se ha presionado la tecla 'C'
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            PlayNextVideo();
        }
    }
}
