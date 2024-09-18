/*
    INSTITUTO TECNOLÓGICO DE COSTA RICA CTLSC
    ESCUELA DE INGENIERÍA EN COMPUTACIÓN
    INTRODUCCIÓNA A LA REALIDAD VIRTUAL - I SEMESTRE 2024
    PROYECTO: ENSEÑA A NIÑOS

    INFORMACIÓN DEL CÓDIGO
    CLASE PARA DETECTAR SI UN OBJETO TOCO EL COLISIONADOR DE UN OBJETO
    AUTOR: JUAN BAUTISTA NÚÑEZ PARRALES
    ÚLTIMA MODIFICACIÓN: JUAN BAUTISTA NÚÑEZ PARRALES - FECHA DE MODIFICACIÓN: 11/06/2024
 */

using UnityEngine;
using System.Collections.Generic;

public class ValidateCubes : MonoBehaviour
{
    public List<GameObject> sockets; // Lista de sockets 
    public LayerMask objectLayer; // Capa de los objetos a detectar

    void Update()
    {
        foreach (GameObject socket in sockets)
        {
            if (!IsSocketOccupied(socket) && CheckForObjectTouching(socket, out GameObject touchingObject))
            {
                //PlaceObjectOnSocket(touchingObject, socket);
                Debug.Log("Objecto puesto sobre el socket");
            }
        }
    }

    bool IsSocketOccupied(GameObject socket)
    {
        // Revisa si el socket ya tiene un objeto hijo (indicador de ocupación)
        return socket.transform.childCount > 0;
    }

    bool CheckForObjectTouching(GameObject socket, out GameObject touchingObject)
    {
        Collider socketCollider = socket.GetComponent<Collider>();
        Collider[] colliders = Physics.OverlapBox(socketCollider.bounds.center, socketCollider.bounds.extents, socket.transform.rotation, objectLayer);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != socket)
            {
                touchingObject = collider.gameObject;
                return true;
            }
        }

        touchingObject = null;
        return false;
    }

    void PlaceObjectOnSocket(GameObject obj, GameObject socket)
    {
        Collider socketCollider = socket.GetComponent<Collider>();
        Collider objectCollider = obj.GetComponent<Collider>();

        Vector3 newPosition = new Vector3(
            socket.transform.position.x,
            socketCollider.bounds.max.y + objectCollider.bounds.extents.y,
            socket.transform.position.z
        );
        obj.transform.position = newPosition;
        Debug.Log("Objecto puesto sobre el socket");
        //obj.transform.parent = socket.transform; // Opcional: Hace que el objeto se convierta en hijo del socket
    }
}