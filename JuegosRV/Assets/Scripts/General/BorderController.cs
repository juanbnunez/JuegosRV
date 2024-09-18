using UnityEngine;

public class BorderController : MonoBehaviour
{
    public enum GameZone
    {
        Lobby,
        MiniGame1,
        MiniGame2,
        MiniGame3,
        MiniGame4
    }

    public GameZone currentZone = GameZone.Lobby; // Zona actual en la que está el jugador

    private GameObject player; // Referencia al jugador
    private bool inputProcessed = false; // Bandera para manejar el input

    void Start()
    {
        // Buscar al jugador por su tag
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Player'.");
        }
    }

    void Update()
    {
        HandleInput();
    }

    // Método para manejar las teclas dependiendo de la zona
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !inputProcessed)
        {
            inputProcessed = true; // Marcamos que el input ha sido procesado

            switch (currentZone)
            {
                case GameZone.Lobby:
                    Debug.Log("Estás en el Lobby, presionaste Space.");
                    break;
                case GameZone.MiniGame1:
                    Debug.Log("Estás en el juego 1, presionaste Space.");
                    break;
                case GameZone.MiniGame2:
                    Debug.Log("Estás en el juego 2, presionaste Space.");
                    break;
                case GameZone.MiniGame3:
                    Debug.Log("Estás en el juego 3, presionaste Space.");
                    break;
                case GameZone.MiniGame4:
                    Debug.Log("Estás en el juego 4, presionaste Space.");
                    break;
            }
        }

        // Reseteamos la bandera si no se está presionando Space
        if (Input.GetKeyUp(KeyCode.Space))
        {
            inputProcessed = false;
        }
    }

    // Detectar cuando el jugador entra en una zona
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha entrado en el trigger: " + this.gameObject.name);

            switch (this.gameObject.name)
            {
                case "LobbyTrigger":
                    currentZone = GameZone.Lobby;
                    Debug.Log("Zona actual: Lobby");
                    break;
                case "MiniGame1Trigger":
                    currentZone = GameZone.MiniGame1;
                    Debug.Log("Zona actual: MiniJuego 1");
                    break;
                case "MiniGame2Trigger":
                    currentZone = GameZone.MiniGame2;
                    Debug.Log("Zona actual: MiniJuego 2");
                    break;
                case "MiniGame3Trigger":
                    currentZone = GameZone.MiniGame3;
                    Debug.Log("Zona actual: MiniJuego 3");
                    break;
                case "MiniGame4Trigger":
                    currentZone = GameZone.MiniGame4;
                    Debug.Log("Zona actual: MiniJuego 4");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha salido de la zona actual.");
        }
    }
}
