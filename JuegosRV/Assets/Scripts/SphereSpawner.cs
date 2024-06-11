using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public int minSpheres = 4;   // Mínimo de esferas a generar
    public int maxSpheres = 6;  // Máximo de esferas a generar
    private Color[] colors = new Color[] {
        Color.yellow, // Amarillo
        Color.red,    // Rojo
        Color.blue,   // Azul
        Color.green   // Verde
    };

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
            Color color = colors[Random.Range(0, colors.Length)];  // Selecciona un color aleatorio de la lista
            GenerateColoredSphereInMeshCollider(meshCollider, color);
        }
    }

    void GenerateColoredSphereInMeshCollider(MeshCollider meshCollider, Color color)
    {
        Vector3 center = meshCollider.bounds.center;
        Vector3 size = meshCollider.bounds.size;

        Vector3 point = new Vector3(
            Random.Range(center.x - size.x / 4, center.x + size.x / 4), // Ajusta estos valores según sea necesario
            center.y, // Ajusta esto para que las esferas se generen a una altura adecuada dentro de la canasta
            Random.Range(center.z - size.z / 4, center.z + size.z / 4)
        );

        if (meshCollider.bounds.Contains(point))
        {
            GameObject sphere = Instantiate(spherePrefab, point, Quaternion.identity, transform);
            float scaleAdjustment = 0.004f;
            sphere.transform.localScale = new Vector3(scaleAdjustment, scaleAdjustment, scaleAdjustment);
            sphere.GetComponent<Renderer>().material.color = color;
            Debug.Log("Esfera " + color.ToString() + " generada en: " + point);
        }
        else
        {
            Debug.Log("Punto generado fuera de los límites: " + point);
        }
    }
}
