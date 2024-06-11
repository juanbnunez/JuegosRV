/*
    INSTITUTO TECNOLÓGICO DE COSTA RICA CTLSC
    ESCUELA DE INGENIERÍA EN COMPUTACIÓN
    INTRODUCCIÓNA A LA REALIDAD VIRTUAL - I SEMESTRE 2024
    PROYECTO: ENSEÑA A NIÑOS

    INFORMACIÓN DEL CÓDIGO
    CLASE PARA GENERAR ESFERAS ALEATORIAS DE DIFERENTES COLORES DENTRO DEL COLISIONADOR DE MALLA DE UN OBJETO
    AUTOR: JUAN BAUTISTA NÚÑEZ PARRALES
    ÚLTIMA MODIFICACIÓN: JUAN BAUTISTA NÚÑEZ PARRALES - FECHA DE MODIFICACIÓN: 10/06/2024
 */

using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public int minSpheres = 4;   // Mínimo de esferas a generar
    public int maxSpheres = 6;  // Máximo de esferas a generar
    public float scaleAdjustment = 0.004f; // Tamaño de la esfera
    private Color[] colors = new Color[] { //Lista de colores
        Color.yellow, // Amarillo
        Color.red,    // Rojo
        Color.blue,   // Azul
        Color.green   // Verde
    };

    // Método de inicio
    void Start()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            Debug.LogError("MeshCollider no encontrado en el objeto!");
            return;
        }

        int numberOfSpheres = Random.Range(minSpheres, maxSpheres + 1);
        for (int i = 0; i < numberOfSpheres; i++)
        {
            Color color = colors[Random.Range(0, colors.Length)];  // Seleccionar un color aleatorio de la lista
            GenerateColoredSphereInMeshCollider(meshCollider, color);
        }
    }

    // Método para generar esferas dentro de los limites del colisionador de malla del objeto
    void GenerateColoredSphereInMeshCollider(MeshCollider meshCollider, Color color)
    {
        Vector3 center = meshCollider.bounds.center; //Centro del colisionador
        Vector3 size = meshCollider.bounds.size; //Tamaño del colisionador

        Vector3 point = new Vector3(
            Random.Range(center.x - size.x / 4, center.x + size.x / 4), // Coordenada x aleatoria dentro de una fracción del ancho del MeshCollider
            center.y, // Coordenada y fija en el centro del MeshCollider
            Random.Range(center.z - size.z / 4, center.z + size.z / 4) // Coordenada z aleatoria dentro de una fracción del largo del MeshCollider
        );

        if (meshCollider.bounds.Contains(point))
        {
            // Instanciar una nueva esfera en la posición generada
            GameObject sphere = Instantiate(spherePrefab, point, Quaternion.identity, transform);
            // Ajustar el tamaño de la esfera
            sphere.transform.localScale = new Vector3(scaleAdjustment, scaleAdjustment, scaleAdjustment);
            // Establecer el color de la esfera
            sphere.GetComponent<Renderer>().material.color = color;
    
            Debug.Log("Esfera " + color.ToString() + " generada en: " + point);
        }
        else
        {
            Debug.Log("Punto generado fuera de los límites: " + point);
        }
    }
}
