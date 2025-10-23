using UnityEngine;
using System.Collections.Generic; 
using System.IO;
using UnityEngine.SceneManagement;

// struct del tesoro
[System.Serializable]
public struct Treasure
{
    public int value;
    public Sprite sprite;
    public Treasure(int val, Sprite spr)
    {
        value = val;
        sprite = spr;
    }
}

public class GameManager : MonoBehaviour
{
    [Header("Stats")]
    public int maxEnergy = 4; // eneria maxima que puede tener el jugador
    public int energy = 4;    // energia actual
    public int lives = 3;     // vidas actuales
    public int treasuresRemaining = 6; // tesoros que faltan por recoger
    public int maxTreasuresThisLevel = 6; //tesoros en el nivel

    [HideInInspector]
    public int currentLevel = 0;

    [Header("Inventario y Puntaje")]
    public int maxInventorySize = 5;
    public List<Treasure> inventory = new List<Treasure>();

    [Header("Timer y Bonificacion")]
    public float maxTimePerLevel = 100f; //tiempo inicial/maximo del nivel (100 segundos)
    [HideInInspector]
    public float currentTime; // tiempo q queda
    public int timeBonusMultiplier = 10; //multiplicador para el puntaje extra

    [HideInInspector]
    public int currentScore = 0;

    public HUDManager hudManager;

    public void CalculateScore()
    {
        currentScore = 0;
        foreach (Treasure treasure in inventory)
        {
            currentScore += treasure.value;
        }
    }

    //solo se usa cuando hay espacio en el inventario
    public void AddTreasure(Treasure newTreasure)
    {
        if (inventory.Count < maxInventorySize)
        {
            inventory.Add(newTreasure);
            currentScore += newTreasure.value;
            CollectTreasure(); //descontar del total del nivel
            Debug.Log($"Tesoro de ${newTreasure.value} agregado. Puntaje: {currentScore}");
        }
    }

    //se llama cuando el inventario esta lleno y el jugador elige reemplazar
    public void ReplaceTreasure(Treasure newTreasure, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < inventory.Count)
        {
            // obtener el valor del tesoro que se va a quitar
            int oldTreasureValue = inventory[slotIndex].value;

            //actualizar inv
            inventory[slotIndex] = newTreasure;

            // actualizar el puntaje total
            currentScore -= oldTreasureValue; //restar el valor viejo (tesoro q tiramos)
            currentScore += newTreasure.value; //sumar el valor nuevo (tesoro q agarramos)

            CollectTreasure();

        }
    }

    public void ConsumeEnergy()
    {
        energy--;
        if (energy < 0) energy = 0;
        Debug.Log("Energia actual: " + energy);
    }

    public void LoseLife()
    {
        lives--;
        energy = maxEnergy;
        Debug.Log("Haz sido tableado, consumiste un suchi pa seguir. Sushis: " + lives);

        if (lives <= 0)
        {
            Debug.Log("Se cansaron de tablearte, te mandaron con San Pedro.");
            Time.timeScale = 0f;
            if (hudManager != null)
            {
                hudManager.ShowGameOverMenu();
            }
        }
    }

    public void CollectTreasure()
    {
        treasuresRemaining--; // restar tesoro
        if (treasuresRemaining < 0) treasuresRemaining = 0;
        Debug.Log("Tesoro recogido. Quedan: " + treasuresRemaining);
    }

    public void ResetTimer()
    {
        currentTime = maxTimePerLevel;
    }

    void Start()
    {

        if (File.Exists(savePath))
        {
            //si el archivo existe, cargamos la partida guardada
            Debug.Log("Partida guardada encontrada. Cargando...");
            LoadGameFromMenu();
        }
        else
        {
            //si no hay archivo, iniciamos una Nueva Partida 
            Debug.Log("Iniciando nueva partida.");
            MapGenerator mapGen = Object.FindFirstObjectByType<MapGenerator>();
            if (mapGen != null)
            {
                mapGen.GenerateAllLevels(); //asegurar que el mapa se genere
                mapGen.SpawnPlayer(); //asegurar que el jugador ese en la escena
                mapGen.LoadLevel(0, true);
            }
        }
    }




    void Update()
    {
        //si el tiempo esta pausado, no descontamos el timer
        if (Time.timeScale == 0f)
            return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; //descontar el tiempo
        }
        else
        {
            currentTime = 0;
            //si el tiempo se acaba, es Game Over
            GameOverByTime();
        }
    }

    //funcion para perder por tiempo
    void GameOverByTime()
    {
        Debug.Log("¡Se acabó el tiempo! Te mandaron con San Pedro por lentitud.");
        Time.timeScale = 0f; //pausa el tiempo del juego
        if (hudManager != null)
        {
            hudManager.ShowGameOverMenu();
        }
    }

    //funciones para poder guardar y cargar el juego
    [HideInInspector]
    public string savePath; //ruta dnd guardaremos el archivo

    void Awake()
    {
        //define la ruta del archivo de guardado
        savePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
    }

    public void SaveGame()
    {
        //crear la estructura de datos
        SaveData data = new SaveData();
    
    //pedir los datos al MapGenerator
    MapGenerator mapGen = Object.FindFirstObjectByType<MapGenerator>();
     if (mapGen == null)
        {
            Debug.LogError("No se puede guardar, MapGenerator no encontrado.");
            return;
        }

        //llenar los datos del GameManager
        data.lives = lives;
        data.energy = energy;
        data.maxEnergy = maxEnergy;
        data.currentScore = currentScore;
        data.inventory = new List<Treasure>(inventory); //copiar el inventario

        //llenar los datos del MapGenerator
        data.currentLevel = mapGen.currentLevel;
        data.currentTime = currentTime;
        data.treasuresRemaining = treasuresRemaining;
        data.maxTreasuresThisLevel = maxTreasuresThisLevel;

        data.playerX = mapGen.playerPos.x;
        data.playerY = mapGen.playerPos.y;

        data.width = mapGen.width;
        data.height = mapGen.height;
        data.levels = mapGen.levels;

        //llenar los datos del mapa ("serializar" la matriz)
        mapGen.SaveMapData(data);
    
    //serializar a JSON y guardar
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(savePath, json);
    PlayerPrefs.SetInt("GameSaved", 1); 
    PlayerPrefs.Save();
    Debug.Log($"Partida guardada exitosamente en: {savePath}");
}


public SaveData LoadGame()
{
    if (!File.Exists(savePath))
    {
        Debug.LogWarning("No existe archivo de guardado.");
        return null;
    }

    //leer el JSON
    string json = File.ReadAllText(savePath);
    
    // deserializar
    SaveData data = JsonUtility.FromJson<SaveData>(json);

    // aplicar los datos 
    Debug.Log("Partida cargada de archivo.");
    return data;
}

//metodo llamado por el menu principal para cargar y aplicar
public void LoadGameFromMenu()
{
    SaveData data = LoadGame();
    if (data == null) return;
    
    //aplicar datos del GameManager
     lives = data.lives;
    energy = data.energy;
    maxEnergy = data.maxEnergy;
    currentScore = data.currentScore;
    inventory = data.inventory; 
    currentTime = data.currentTime; 
    treasuresRemaining = data.treasuresRemaining;
    maxTreasuresThisLevel = data.maxTreasuresThisLevel; 
    
    //le decimos al MapGenerator que aplique los datos restantes
    MapGenerator mapGen = Object.FindFirstObjectByType<MapGenerator>();
    if (mapGen != null)
    {
        mapGen.ApplyLoadedData(data);
    }
    
    Debug.Log("Juego reanudado desde el guardado.");
    
    HUDManager hud = Object.FindFirstObjectByType<HUDManager>();
    if(hud != null) hud.UpdateUHD(this); 
}

    public void GoToMainMenu()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("MenuPrincipal"); 
}

}