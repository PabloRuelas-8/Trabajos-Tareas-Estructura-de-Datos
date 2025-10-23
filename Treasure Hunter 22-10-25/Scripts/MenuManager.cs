using UnityEngine;
using UnityEngine.SceneManagement; 
using System.IO;                   

public class MenuManager : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string gameSceneName = "MainScene"; 

    [Header("Referencias UI del Menú")]
    public GameObject loadButton; 

    private string savePath; //ruta donde se guarda el archivo

    void Awake()
    {
        //definir la ruta del archivo de guardado
        savePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
    }

    void Start()
    {
        Time.timeScale = 1f; 
        
     //revisar si el archivo de guardado existe
        if (loadButton != null)
        {
            loadButton.SetActive(File.Exists(savePath));
        }
    }

    public void NewGame()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Archivo de guardado anterior eliminado.");
        }
        SceneManager.LoadScene(gameSceneName);
    }

    //cargar Partida
    public void LoadGame()
    {
       
        //revisar si hay datos para cargar
        if (File.Exists(savePath))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("Intento de carga fallido: No se encontró archivo de guardado.");
        }
    }

    //boton salir
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}