using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MapGenerator mapGen;
    private SpriteRenderer spriteRenderer;

    public Sprite playerIdle;
    public Sprite playerUp;
    public Sprite playerDown;
    public Sprite playerLeft;
    public Sprite playerRight;

    public float idleDelay = 1f; //tiempo en segundos para volver a Idle
    private float timeSinceLastMove; //cronometro para checar idle
    private HUDManager hudManager; //referencia al HUD
    void Start()
    {
        mapGen = Object.FindFirstObjectByType<MapGenerator>();
        if (mapGen == null)
            Debug.LogError("MapGenerator no encontrado");

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = playerIdle;
        hudManager = Object.FindFirstObjectByType<HUDManager>();
    }

    void Update()
    {
        //evitar movimiento si el juego esta pausado
        if (Time.timeScale == 0f)
        {
            return;
        }



        timeSinceLastMove += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            spriteRenderer.sprite = playerUp;
            mapGen.TryMovePlayer(Vector2Int.up);
            timeSinceLastMove = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            spriteRenderer.sprite = playerDown;
            mapGen.TryMovePlayer(Vector2Int.down);
            timeSinceLastMove = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.sprite = playerLeft;
            mapGen.TryMovePlayer(Vector2Int.left);
            timeSinceLastMove = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRenderer.sprite = playerRight;
            mapGen.TryMovePlayer(Vector2Int.right);
            timeSinceLastMove = 0f;
        }

        //pasando 2 segs reiniciamos sprite a idle
        if (timeSinceLastMove >= idleDelay)
        {
            spriteRenderer.sprite = playerIdle;
        }

        //menu de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseMenu();
        }

    }


    void OpenPauseMenu()
    {
        //obtener la referencia si no la tienes
        if (hudManager == null)
        {
            hudManager = Object.FindFirstObjectByType<HUDManager>();
        }

        if (hudManager != null && hudManager.PauseMenuPanel != null)
        {
            // Pausar
            Time.timeScale = 0f;
            hudManager.PauseMenuPanel.SetActive(true);
        }




    }
}