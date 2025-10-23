using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
    public TMP_Text energyText;
    public TMP_Text livesText;
    public TMP_Text treasuresText;
    public TMP_Text levelText;

    [Header("Puntaje y Tesoro")]
    public TMP_Text scoreText; 
    [Header("Timer")]
    public TMP_Text timerText;    
    public GameObject treasureMenuPanel; 
    public TMP_Text newTreasureValueText; 
    public UnityEngine.UI.Image newTreasureSprite;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public GameObject PauseMenuPanel;

    public List<UnityEngine.UI.Button> inventorySlotsButtons = new List<UnityEngine.UI.Button>(); 
    
    private GameManager gm;
    
    public void UpdateUHD(GameManager gm)
    {
        energyText.text = $"Energia: {gm.energy}/{gm.maxEnergy}";
        livesText.text = $"Vidas: {gm.lives}";
        treasuresText.text = $"Tesoros: {gm.treasuresRemaining}/{gm.maxTreasuresThisLevel}";
        levelText.text = $"Nivel: {gm.currentLevel + 1}";

        scoreText.text = $"Puntaje: {gm.currentScore}";

        for (int i = 0; i < gm.maxInventorySize; i++)
        {
            if (i < gm.inventory.Count)
            {
                //muestra el sprite del tesoro y su valor
                inventorySlotsButtons[i].GetComponent<UnityEngine.UI.Image>().sprite = gm.inventory[i].sprite;
                //asumiendo que cada botón de slot tiene un TMP_Text hijo
                inventorySlotsButtons[i].GetComponentInChildren<TMP_Text>().text = $"{gm.inventory[i].value}";
                inventorySlotsButtons[i].gameObject.SetActive(true);
            }
            else
            {
                //esconde los slots vacíos
                inventorySlotsButtons[i].gameObject.SetActive(false);
            }
        }
        
        //actualizar timer
        int seconds = Mathf.CeilToInt(gm.currentTime); 
         timerText.text = $"Tiempo: {seconds}";

    }

    //metodo para mostrar el menu de reemplazo
    public void ShowTreasureMenu(Treasure newTreasure)
    {
        treasureMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
        
        newTreasureValueText.text = $"Nuevo: {newTreasure.value}";
        newTreasureSprite.sprite = newTreasure.sprite;
        
        //actualizar el texto de los botones del menu para saber que reemplazas
        GameManager gm = FindFirstObjectByType<GameManager>();
        for(int i = 0; i < gm.inventory.Count; i++)
        {
            //busca el texto de cada boton de reemplazo (son los mismos slots)
            TMP_Text buttonText = inventorySlotsButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = $"Cambiar por\n{gm.inventory[i].value}";
        }
    }

    //metodo para esconder el menu 
    public void HideTreasureMenu()
    {
        if (treasureMenuPanel != null)
        {
            treasureMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f; //reanuda el juego
    }

    public void ShowGameOverMenu()
    {
        if (gameOverPanel != null)
        {
         gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f; 
    }

    void Start()
    {
        //obtener la referencia al GameManager una sola vez
        gm = FindFirstObjectByType<GameManager>(); 
    
   
    }


    void Update()
    {
        if (gm != null)
        {
            UpdateUHD(gm);
        }
    }

    public void ClosePauseMenu()
    {
    if (PauseMenuPanel != null)
        {
            PauseMenuPanel.SetActive(false);
        }
    
        //restaurar el tiempo del juego
        Time.timeScale = 1f; 
    }



}
