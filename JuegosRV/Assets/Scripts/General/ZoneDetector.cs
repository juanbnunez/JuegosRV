using UnityEngine;

public class ZoneDetector : MonoBehaviour
{
    // Method called when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Show a message when the player enters the zone
            Debug.Log("Player has entered the zone!");
        }
    }

    // Method called when another collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Check if the object that exited is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Show an alert when the player exits the zone
            Debug.Log("Player has left the zone! Alert!");
        }
    }
}
