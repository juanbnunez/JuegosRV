using UnityEngine;

/// <summary>
/// This script resets the object's position to its original position if it falls below a certain Y threshold.
/// </summary>
public class SphereAnchor : MonoBehaviour
{
    /// <summary>
    /// The original position of the object.
    /// </summary>
    private Vector3 originalPosition;

    /// <summary>
    /// The Y-axis threshold below which the object will reset to its original position.
    /// </summary>
    public float fallThresholdY = 0.3f;

    /// <summary>
    /// Called before the first frame update. Stores the initial position of the object.
    /// </summary>
    void Start()
    {
        // Store the initial position of the object
        originalPosition = transform.position;
    }

    /// <summary>
    /// Called once per frame. Checks if the object has fallen below the threshold.
    /// If it has, the object is teleported back to its original position.
    /// </summary>
    void Update()
    {
        // Check if the object has fallen below the Y threshold
        if (transform.position.y < fallThresholdY)
        {
            // If it has fallen, teleport it back to its original position
            transform.position = originalPosition;
        }
    }
}
