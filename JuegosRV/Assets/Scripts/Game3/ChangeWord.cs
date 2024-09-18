/*
    INSTITUTO TECNOLÓGICO DE COSTA RICA CTLSC
    ESCUELA DE INGENIERÍA EN COMPUTACIÓN
    INTRODUCCIÓNA A LA REALIDAD VIRTUAL - I SEMESTRE 2024
    PROYECTO: ENSEÑA A NIÑOS

    INFORMACIÓN DEL CÓDIGO
    CLASE PARA GENERAR LOS CUBOS EN LA PLATAFORMA INICIAL
    AUTOR: JUAN BAUTISTA NÚÑEZ PARRALES
    ÚLTIMA MODIFICACIÓN: JUAN BAUTISTA NÚÑEZ PARRALES - FECHA DE MODIFICACIÓN: 10/09/2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

// Clase serializable para almacenar la asociación entre letra y prefab
[System.Serializable]
public class LetterPrefabAssociation
{
    public char letter;
    public GameObject prefab;
}

public class ChangeWord : MonoBehaviour
{
    // Atributos -------------------------------------------------------------
    public float scaleAdjustment; // Ajuste de escala
    public float heightAdjustment; // Ajuste de altura
    public List<string> wordList = new List<string> { }; // Lista de palabras

    private bool isPlayerInZone = false; // Variable para validar si el jugador está en la zona

    // Lista de asociaciones entre letras y prefabs
    public List<LetterPrefabAssociation> letterPrefabAssociations;

    // Diccionario para acceso rápido a los prefabs por letra
    private Dictionary<char, GameObject> letterToPrefab;
    private List<GameObject> currentInstances; // Lista para almacenar los cubos generados actualmente
    public int currentWordIndex; // Índice de la palabra actual en la lista

    // Métodos ---------------------------------------------------------------

    void Start()
    {
        currentInstances = new List<GameObject>(); // Inicializar la lista de instancias actuales
        currentWordIndex = 0; // Iniciar con la primera palabra

        // Inicializar el diccionario de asociaciones
        InitializeLetterToPrefab();

        // Generar los cubos
        GenerateCube();

    }

    void Update()
    {
        // Solo ejecutar si el jugador está en la zona
        if (isPlayerInZone)
        {
            // Detectar si se ha presionado la tecla 'A'
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                ClearCubes(); // Borrar los cubos actuales
                currentWordIndex = (currentWordIndex + 1) % wordList.Count; // Avanzar al siguiente índice de palabra
                GenerateCube(); // Generar los cubos de la siguiente palabra
            }
        }
    }


    // Método para buscar y añadir en una lista de listas, las letras de una palabra, dentro de una lista de palabras
    List<List<char>> SearchLetter()
    {
        List<List<char>> allLetters = new List<List<char>>();

        foreach (string word in wordList)
        {
            List<char> letters = new List<char>(word);
            allLetters.Add(letters);
        }

        return allLetters;
    }

    // Método para asociar una letra de una lista a un prefab
    void InitializeLetterToPrefab()
    {
        letterToPrefab = new Dictionary<char, GameObject>();
        foreach (var association in letterPrefabAssociations)
        {
            if (!letterToPrefab.ContainsKey(association.letter))
            {
                letterToPrefab.Add(association.letter, association.prefab);
            }
        }
    }

    // Método para mezclar aleatoriamente una lista
    List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    // Método para borrar los cubos actuales
    void ClearCubes()
    {
        foreach (GameObject instance in currentInstances)
        {
            Destroy(instance);
        }
        currentInstances.Clear();
    }

    // Método para generar los cubos
    void GenerateCube()
    {
        List<Transform> sockets = new List<Transform>();

        // Obtener todos los objetos hijo directos
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            //Debug.Log("Hijo directo: " + child.gameObject.name);
            sockets.Add(child);
        }

        // Llamar a SearchLetter() para obtener las letras de las palabras
        List<List<char>> allLetters = SearchLetter();

        // Asegurarse de que hay al menos una palabra en la lista
        if (allLetters.Count > 0)
        {
            // Obtener las letras de la palabra actual
            List<char> currentWordLetters = allLetters[currentWordIndex];
            // Mezclar aleatoriamente las letras
            currentWordLetters = ShuffleList(currentWordLetters);
            int wordLength = currentWordLetters.Count;

            // Iterar sobre las letras de la palabra actual y los sockets
            for (int i = 0; i < wordLength && i < sockets.Count; i++)
            {
                char letter = currentWordLetters[i];
                if (letterToPrefab.ContainsKey(letter))
                {
                    GameObject prefab = letterToPrefab[letter];
                    // Calcular la posición justo encima del socket
                    Vector3 positionAboveSocket = sockets[i].position + new Vector3(0, heightAdjustment, 0);
                    // Instanciar el prefab en la posición calculada
                    GameObject instance = Instantiate(prefab, positionAboveSocket, Quaternion.identity);
                    // Ajustar la escala del prefab
                    instance.transform.localScale = new Vector3(scaleAdjustment, scaleAdjustment, scaleAdjustment);
                    currentInstances.Add(instance); // Añadir la instancia a la lista de cubos actuales
                }
                else
                {
                    Debug.LogWarning($"No hay prefab asociado para la letra: {letter}");
                }
            }
        }
        else
        {
            Debug.LogWarning("La lista de palabras está vacía.");
        }
    }

    // Detectar si el jugador entra en la zona
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene el tag "Player"
        {
            isPlayerInZone = true;
            while(1 == 1){ Debug.LogWarning("Jugador en la zona"); }
            
        }
    }

    // Detectar si el jugador sale de la zona
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }



}
