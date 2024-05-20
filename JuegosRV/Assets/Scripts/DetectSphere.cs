using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI

public class DetectSphere : MonoBehaviour
{
    public Color referenceColor;  // Color de referencia establecido en el Inspector
    public Text sphereCountText;  // Referencia al texto UI para mostrar el conteo de esferas
    private int sphereCount = 0;  // Contador de esferas

    private void Start()
    {
        // Inicializa el texto con el conteo actual
        UpdateSphereCountText();
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer renderer = other.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color objectColor = renderer.material.color;  // Obtiene el color del objeto que entra
            if (ColorsAreSimilar(referenceColor, objectColor))
            {
                sphereCount++;
                UpdateSphereCountText();
                Debug.Log("Un objeto del mismo color ha entrado en la canasta: " + other.gameObject.name);
            }
            else
            {
                Debug.Log("Un objeto de diferente color ha entrado en la canasta: " + other.gameObject.name);
            }
        }
        else
        {
            Debug.Log("El objeto entrante no tiene un componente Renderer.");
        }
    }

    // Método para comparar colores con cierto margen de tolerancia para diferencias menores
    private bool ColorsAreSimilar(Color a, Color b, float tolerance = 0.1f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance && Mathf.Abs(a.g - b.g) < tolerance && Mathf.Abs(a.b - b.b) < tolerance;
    }

    // Método para actualizar el texto UI con el conteo de esferas
    private void UpdateSphereCountText()
    {
        sphereCountText.text = "Esferas en la canasta: " + sphereCount;
    }
}
