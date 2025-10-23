using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    //datos del Juego (GameManager)
    public int lives;
    public int energy;
    public int maxEnergy;
    public int currentScore;
    public List<Treasure> inventory = new List<Treasure>(); 

    //datos del Mapa (MapGenerator)
    public int currentLevel;
    public float currentTime; 
    public int treasuresRemaining;
    public int maxTreasuresThisLevel;

    //posición del Jugador
    public int playerX;
    public int playerY;

    //guardar el estado de cada tile (Tipo y datos del tesoro)
    public List<TileRow> mapDataList = new List<TileRow>();
    public List<TreasureDataRow> treasureDataList = new List<TreasureDataRow>();
    
    //dimensiones
    public int width;
    public int height;
    public int levels;
    
    //variable para ver si se guardo la partida
    public bool isGameSaved = false;
}

//ayudante para serializar la matriz TileType[,,] 
[System.Serializable]
public class TileRow
{
    public List<int> tiles = new List<int>(); //guardar el TileType como int
}

//ayudante para serializar la matriz TreasureData[,,]
[System.Serializable]
public class TreasureDataRow
{
    public List<Treasure> treasures = new List<Treasure>(); 
}

