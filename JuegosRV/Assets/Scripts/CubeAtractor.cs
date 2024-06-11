/*
    INSTITUTO TECNOLÓGICO DE COSTA RICA CTLSC
    ESCUELA DE INGENIERÍA EN COMPUTACIÓN
    INTRODUCCIÓNA A LA REALIDAD VIRTUAL - I SEMESTRE 2024
    PROYECTO: ENSEÑA A NIÑOS

    INFORMACIÓN DEL CÓDIGO
    CLASE PARA ATRAER UN CUBO A UNA SUPERFICIE
    AUTOR: JUAN BAUTISTA NÚÑEZ PARRALES
    ÚLTIMA MODIFICACIÓN: JUAN BAUTISTA NÚÑEZ PARRALES - FECHA DE MODIFICACIÓN: 10/06/2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAtractor : MonoBehaviour
{
    public GameObject Cube;

    // Start is called before the first frame update
    void Start()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            Debug.LogError("MeshCollider no encontrado en el objeto!");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AtractObject()
    {

    }
}
