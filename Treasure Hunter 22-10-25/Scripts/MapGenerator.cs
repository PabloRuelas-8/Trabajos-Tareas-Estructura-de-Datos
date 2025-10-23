using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class MapGenerator : MonoBehaviour
{

    //dimensiones 
    public int width = 20;
    public int height = 20;
    public int levels = 5;

    //agregamos los prefabs
    public GameObject groundPrefab;
    public GameObject wallPrefab;
    public GameObject trapPrefab;
    public GameObject treasurePrefab;
    public GameObject exitPrefab;
    public GameObject playerPrefab;
    public GameObject fogPrefab;

    //probabilidades de colocacion
    public int baseTrapChance = 5; //probabilidad de trampa en el Nivel 0
    public int trapChanceIncreasePerLevel = 2; //lo q sube la probabilidad por nivel
    public int baseTreasureCount = 6; // tesoros en el Nivel 0

    [Header("Niebla")]
    public int revealRadius = 2; // cuantos tiles alrededor del jugador se revelan


    [Header("Sprites de Tesoro")]
    public Sprite treasureSprite100_300;
    public Sprite treasureSprite400_800;
    public Sprite treasureSprite900_1000;


    //estructura de datos logica
    public enum TileType { Empty, Wall, Trap, Treasure, Exit } //tipos de tiles
    public TileType[,,] mapData; //matriz logica x,y,z
    public GameObject[,,] mapObjects; // matriz visual de GameObjects x,y,z
    public GameObject[,,] fogObjects; // matriz visual de la niebla

    public Treasure[,,] treasureData; //matriz para almacenar los datos (valor/sprite)
    public Treasure pendingTreasure; //tesoro temporal en el menú de reemplazo


    //srray para el algoritmo de laberinto
    public bool[,,] visited;
    // array para contar tesoros por nivel
    public int[] treasuresPerLevel;

    //runtime (al momento d jugar)
    public int currentLevel = 0; // nivel activo
    public Vector2Int playerPos = new Vector2Int(1, 1); //empezar en la casilla (1,1)
    public GameObject playerObj; //referencia al jugador

    public GameManager gameManager; //referencia al GameManager
    public HUDManager hudManager; //referencia al HUDManager

    //variable para el combo de energía
    private int movementCombo = 0;

    public void Start()
    {
        //revisa q haya un gamemanager asignado, si no busca uno
        if (gameManager == null)
            gameManager = Object.FindFirstObjectByType<GameManager>();
        //revisa q haya un hudManager asignado, si no busca uno
        if (hudManager == null)
            hudManager = Object.FindFirstObjectByType<HUDManager>();

        //instancian matrices con las dimensiones
        mapData = new TileType[width, height, levels];
        mapObjects = new GameObject[width, height, levels];
        visited = new bool[width, height, levels];
        treasuresPerLevel = new int[levels];
        fogObjects = new GameObject[width, height, levels];
        treasureData = new Treasure[width, height, levels];

        //generamos niveles
        GenerateAllLevels();
        //agregamos el jugador
        SpawnPlayer();
        //cargamos el primer nivel y colocamos el jugador
        LoadLevel(0, true);
        //iniciar el timer
        gameManager.ResetTimer();
        //actualizamos la HUD initial con valores del GameManager
        hudManager.UpdateUHD(gameManager);

    }

    public void GenerateAllLevels()
    {
        //limpiamos duplicados de GameObjects
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        for (int z = 0; z < levels; z++)
            GenerateLevel(z); //genero capa z
    }

    void GenerateLevel(int z)
    {
        //llenar todo el mapa de paredes y resetear las visitadas(para llenarlas con niebla d nuevo)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                mapData[x, y, z] = TileType.Wall;
                visited[x, y, z] = false;
            }
        }

        //creacion de laberinto
        CarveMaze(1, 1, z);


        //identificar posibles lugares para las trampas (finales sin salida,cruces)
        List<Vector2Int> emptyTiles = new List<Vector2Int>();
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        List<Vector2Int> junctions = new List<Vector2Int>();

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (mapData[x, y, z] == TileType.Empty)
                {
                    // guardar todos los suelos(menos el spawn)
                    if (!(x == 1 && y == 1))
                    {
                        emptyTiles.Add(new Vector2Int(x, y));
                    }

                    // contar vecinos que no son pared
                    int emptyNeighbors = CountEmptyNeighbors(x, y, z);

                    //si solo tiene 1 vecino suelo, es callejon sin salida
                    if (emptyNeighbors == 1 && !(x == 1 && y == 1))
                    {
                        deadEnds.Add(new Vector2Int(x, y));
                    }
                    // si tiene 3 o 4 vecinos suelo, es un cruce
                    else if (emptyNeighbors >= 3)
                    {
                        junctions.Add(new Vector2Int(x, y));
                    }
                }
            }
        }

        //colocar salida en un callejon sin salida aleatorio, lejos del inicio
        Vector2Int exitPos = Vector2Int.zero;
        List<Vector2Int> farDeadEnds = deadEnds.FindAll(pos => pos.x >= width / 2 && pos.y >= height / 2);

        if (farDeadEnds.Count > 0)
        {
            exitPos = farDeadEnds[Random.Range(0, farDeadEnds.Count)];
        }
        else if (deadEnds.Count > 0) //si no hay lejanos, agarramos uno cualquiera
        {
            exitPos = deadEnds[Random.Range(0, deadEnds.Count)];
        }
        else //si no hay callejones, ponemos la salida cerca de la esquina
        {
            exitPos = new Vector2Int(width - 2, height - 2);
            //asegurar que sea suelo
            if (mapData[exitPos.x, exitPos.y, z] == TileType.Wall) mapData[exitPos.x, exitPos.y, z] = TileType.Empty;
        }
        mapData[exitPos.x, exitPos.y, z] = TileType.Exit;
        emptyTiles.Remove(exitPos); //la quitamos de los lugares disponibles
        deadEnds.Remove(exitPos); //la quitamos tambien de deadEnds


        //colocar tesoros
        //revolver la lista de lugares disponibles
        for (int i = 0; i < emptyTiles.Count; i++)
        {
            int j = Random.Range(i, emptyTiles.Count);
            Vector2Int temp = emptyTiles[i];
            emptyTiles[i] = emptyTiles[j];
            emptyTiles[j] = temp;
        }
        int treasureCountForThisLevel = baseTreasureCount - z;
        if (treasureCountForThisLevel < 1) treasureCountForThisLevel = 1;
        int treasuresPlaced = 0;
        List<Vector2Int> treasureLocations = new List<Vector2Int>(); //guardamos donde quedaron

        for (int i = 0; i < treasureCountForThisLevel; i++)
        {
            if (i >= emptyTiles.Count) break; //si no hay mas lugares
            Vector2Int treasurePos = emptyTiles[i];
            mapData[treasurePos.x, treasurePos.y, z] = TileType.Treasure;
            //guardar dato del tesoro
            treasureData[treasurePos.x, treasurePos.y, z] = GetRandomTreasure();
            treasureLocations.Add(treasurePos); //guardar la ubicacion
            treasuresPlaced++;
        }
        treasuresPerLevel[z] = treasuresPlaced;
        //quitamos los lugares de tesoros de la lista de emptyTiles
        foreach (var tPos in treasureLocations) emptyTiles.Remove(tPos);


        //colocar trampas
        int currentTrapChance = baseTrapChance + (z * trapChanceIncreasePerLevel);
        List<Vector2Int> placedTraps = new List<Vector2Int>(); //para no poner dos en el mismo lugar

        //para poner en callejones sin salida
        foreach (Vector2Int deadEndPos in deadEnds)
        {
            if (Random.Range(0, 100) < currentTrapChance * 0.8f)
            {
                mapData[deadEndPos.x, deadEndPos.y, z] = TileType.Trap;
                placedTraps.Add(deadEndPos);
                emptyTiles.Remove(deadEndPos);
            }
        }


        //trampas en cruces
        foreach (Vector2Int junctionPos in junctions)
        {


            //ver que no haya una trampa ahi
            if (placedTraps.Contains(junctionPos)) continue;

            //y tmb q siga siendo suelo
            if (mapData[junctionPos.x, junctionPos.y, z] != TileType.Empty) continue;


            if (Random.Range(0, 100) < currentTrapChance * 0.2f)
            {
                mapData[junctionPos.x, junctionPos.y, z] = TileType.Trap;
                placedTraps.Add(junctionPos);
                emptyTiles.Remove(junctionPos);
            }
        }


        //asegurar que el spawn (1,1) este limpio 
        mapData[1, 1, z] = TileType.Empty;

        // ahora si, creamos los GameObjects (mapa y niebla)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                GameObject prefabToUse = null;

                switch (mapData[x, y, z])
                {
                    case TileType.Wall: prefabToUse = wallPrefab; break;
                    case TileType.Trap: prefabToUse = trapPrefab; break;
                    case TileType.Treasure:
                        prefabToUse = treasurePrefab;
                        //instanciar el GameObject de Suelo
                        GameObject groundObj = Instantiate(groundPrefab, pos, Quaternion.identity, transform);
                        groundObj.name = $"Ground_{x}_{y}_L{z}";
                        groundObj.SetActive(false);

                        //crear objeto visual
                        GameObject treasureItem = Instantiate(treasurePrefab, pos, Quaternion.identity, transform);

                        //asignar sprite especifico
                        if (treasureItem.GetComponent<SpriteRenderer>() != null)
                        {
                            // obtener el sprite que se genero y guardo arriba
                            treasureItem.GetComponent<SpriteRenderer>().sprite = treasureData[x, y, z].sprite;
                        }

                        treasureItem.name = $"Treasure_{x}_{y}_L{z}";
                        treasureItem.SetActive(false);
                        // guardamos la referencia del objeto
                        mapObjects[x, y, z] = treasureItem;
                        break;
                    case TileType.Exit: prefabToUse = exitPrefab; break;
                    case TileType.Empty: default: prefabToUse = groundPrefab; break;
                }

                GameObject obj = Instantiate(prefabToUse, pos, Quaternion.identity, transform);
                obj.name = $"Tile_{x}_{y}_L{z}";

                obj.SetActive(false); //los dejamos apagados
                mapObjects[x, y, z] = obj;

                //crear tile de niebla por encima
                if (fogPrefab != null)
                {
                    // crear la niebla en la misma posicion
                    GameObject fogTile = Instantiate(fogPrefab, pos, Quaternion.identity, transform);
                    fogTile.name = $"Fog_{x}_{y}_L{z}";
                    fogTile.SetActive(false); // dejar apagada
                    fogObjects[x, y, z] = fogTile; // guardar su referencia
                }
            }
        }
    }


    Treasure GetRandomTreasure()
    {
        //generar valor entre 100 y 1000, de 100 en 100
        int value = Random.Range(1, 11) * 100;
        Sprite sprite;

        if (value >= 100 && value <= 300)
            sprite = treasureSprite100_300;
        else if (value >= 400 && value <= 800)
            sprite = treasureSprite400_800;
        else // 900 o 1000
            sprite = treasureSprite900_1000;

        return new Treasure(value, sprite);
    }



    //funcion de creacion de laberinto
    void CarveMaze(int x, int y, int z)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(x, y));
        visited[x, y, z] = true;
        mapData[x, y, z] = TileType.Empty;

        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 2),  // Arriba
            new Vector2Int(0, -2), // Abajo
            new Vector2Int(2, 0),  // Derecha
            new Vector2Int(-2, 0)  // Izquierda
        };

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            List<Vector2Int> neighbors = new List<Vector2Int>();

            //revolver las direcciones
            for (int i = 0; i < directions.Count; i++)
            {
                int j = Random.Range(i, directions.Count);
                Vector2Int temp = directions[i];
                directions[i] = directions[j];
                directions[j] = temp;
            }

            // checar los vecinos
            foreach (Vector2Int dir in directions)
            {
                int nx = current.x + dir.x;
                int ny = current.y + dir.y;

                // checar si el vecino está dentro del mapa (lejos de los bordes)
                if (nx > 0 && nx < width - 1 && ny > 0 && ny < height - 1 && !visited[nx, ny, z])
                {
                    neighbors.Add(dir); // agregar  dirección como valida
                }
            }

            if (neighbors.Count > 0)
            {
                //si hay vecinos, elegir uno al azar
                Vector2Int chosenDir = neighbors[0];
                int nx = current.x + chosenDir.x;
                int ny = current.y + chosenDir.y;

                //tumbar pared de en medio
                int wallX = current.x + (chosenDir.x / 2);
                int wallY = current.y + (chosenDir.y / 2);
                mapData[wallX, wallY, z] = TileType.Empty;

                //marcar vecino y hacerlo suelo
                visited[nx, ny, z] = true;
                mapData[nx, ny, z] = TileType.Empty;

                // meter el vecino a la pila para explorarlo despues
                stack.Push(new Vector2Int(nx, ny));
            }
            else
            {
                //si no hay vecinos, la sacamos de la pila.
                stack.Pop();
            }
        }
    }

    //funcion para contar vecinos vacios 
    int CountEmptyNeighbors(int x, int y, int z)
    {
        int count = 0;
        //revisar las 4 direcciones
        if (mapData[x, y + 1, z] == TileType.Empty) count++;
        if (mapData[x, y - 1, z] == TileType.Empty) count++;
        if (mapData[x + 1, y, z] == TileType.Empty) count++;
        if (mapData[x - 1, y, z] == TileType.Empty) count++;
        return count;
    }



    public void SpawnPlayer()
    {

        GameObject existingPlayer = GameObject.Find("Player");
        if (existingPlayer != null)
        {
            Destroy(existingPlayer);
        }


        // mover el spawn a (1,1) 
        playerPos = new Vector2Int(1, 1);
        playerObj = Instantiate(playerPrefab, new Vector3(playerPos.x, playerPos.y, 0), Quaternion.identity);

        playerObj.name = "Player"; //nombre para falicitar jerarquia
        CameraManager camFollow = Camera.main.GetComponent<CameraManager>();
        if (camFollow != null)
        {
            camFollow.objetivo = playerObj.transform;
        }

    }

    public bool TryMovePlayer(Vector2Int dir)
    {
        //posicion destino sumando la direccion
        Vector2Int newPos = playerPos + dir;

        //evitar salir del mapa
        if (newPos.x < 0 || newPos.x >= width || newPos.y < 0 || newPos.y >= height)
        {
            Debug.Log("No puedes salir de canteras por ahi pa, mucho cholo.");
            return false;
        }

        //obtener el tipo de tile en la nueva posicion
        TileType target = mapData[newPos.x, newPos.y, currentLevel];

        //si es pared
        if (target == TileType.Wall)
        {
            gameManager.ConsumeEnergy(); //consumir energia
            Debug.Log("Te chocaste con una pared.");

            //Si choca con pared, reseteamos el combo
            movementCombo = 0;

            GameObject wallFogTile = fogObjects[newPos.x, newPos.y, currentLevel];
            if (wallFogTile != null && wallFogTile.activeSelf)
            {
                wallFogTile.SetActive(false); // Apagamos la niebla de la pared
            }

            hudManager.UpdateUHD(gameManager);
            return false;
        }

        //si no es pared, actualizar posicion logica y visual del player
        playerPos = newPos; //actualizar posicion logica
        playerObj.transform.position = new Vector3(playerPos.x, playerPos.y, 0); //movemos player

        //revelar el area despues de movernos 
        RevealArea(playerPos);

        //si no fue pared, sumamos al combo
        movementCombo++;
        // si el combo llega a 3, damos energía y reseteamos
        if (movementCombo >= 3)
        {
            gameManager.energy++; // Damos 1 de energia
            // Nos aseguramos de no pasarnos del maximo
            if (gameManager.energy > gameManager.maxEnergy)
            {
                gameManager.energy = gameManager.maxEnergy;
            }
            movementCombo = 0; // Reseteamos el combo
            Debug.Log("COMBO! +1 de Energía por 3 pasos.");
        }


        // si al moverse la energia llego a 0 (o menos), quitamos una vida
        if (gameManager.energy <= 0)
        {
            gameManager.LoseLife();
        }

        //manejo del tile donde cayo el player
        HandleTileAt(playerPos);
        //actualizacion de HUD con los nuevos valores
        hudManager.UpdateUHD(gameManager);

        return true;
    }

    //procesar lo q pasa en la celda q cayo el player
    void HandleTileAt(Vector2Int pos)
    {
        TileType t = mapData[pos.x, pos.y, currentLevel];

        //si el tile era tesoro:
        if (t == TileType.Treasure)
        {
            Treasure newTreasure = treasureData[pos.x, pos.y, currentLevel];

            //inventario NO lleno, lo recogemos
            if (gameManager.inventory.Count < gameManager.maxInventorySize)
            {
                gameManager.AddTreasure(newTreasure);

                //limpiar el mapa
                mapData[pos.x, pos.y, currentLevel] = TileType.Empty;
                Destroy(mapObjects[pos.x, pos.y, currentLevel]);
                PlaceGroundTile(pos);
            }
            //inventario LLENO: mostrar menu
            else
            {
                pendingTreasure = newTreasure; //guardar temporalmente
                hudManager.ShowTreasureMenu(newTreasure);
                return; //salir pq el juego se pauso
            }
        }

        //si el tile era trap:
        else if (t == TileType.Trap)
        {
            gameManager.LoseLife(); //perder una vida
            mapData[pos.x, pos.y, currentLevel] = TileType.Empty; //cambiar a vacio
            Destroy(mapObjects[pos.x, pos.y, currentLevel]);
            GameObject ground = Instantiate(groundPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity, transform);
            ground.SetActive(true);
            mapObjects[pos.x, pos.y, currentLevel] = ground;
        }

        //si el tile era salida
        else if (t == TileType.Exit)
        {
            Debug.Log("LLegaste a la salida del nivel");
            //calcular la bonificacion con tiempo restante
            int secondsRemaining = Mathf.FloorToInt(gameManager.currentTime);
            int timeBonus = secondsRemaining * gameManager.timeBonusMultiplier;

            gameManager.currentScore += timeBonus;
            hudManager.UpdateUHD(gameManager);
            Debug.Log($"Bonificación de Tiempo: {secondsRemaining}s x {gameManager.timeBonusMultiplier} = {timeBonus} puntos extra.");



            if (currentLevel < levels - 1)
            {
                gameManager.lives++;
                Debug.Log("Ganaste un sushi extra! Vidas: " + gameManager.lives);
                gameManager.maxEnergy++;
                Debug.Log("Tu estamina máxima aumentó! Max: " + gameManager.maxEnergy);
                gameManager.energy = gameManager.maxEnergy;
                LoadLevel(currentLevel + 1, true);
            }
            else
            {
                Debug.Log("Felicidades, escapaste de Canteras. Solo queda esperar el Mochis-San Blas.");
                Time.timeScale = 0f;
            }
        }
    }



    //funcion para el boton rechazar
    public void DeclineTreasure()
    {
        Debug.Log($"Tesoro de ${pendingTreasure.value} rechazado.");
        pendingTreasure = new Treasure(); //limpiamr la variable temporal
        hudManager.HideTreasureMenu(); //despausar el juego
        hudManager.UpdateUHD(gameManager);
    }

    //funcion para los botones de slots 
    public void ReplaceInventorySlot(int slotIndex)
    {
        //reemplazar en GameManager
        gameManager.ReplaceTreasure(pendingTreasure, slotIndex);

        //limpiar el mapa, el tesoro recogido se borra del suelo
        mapData[playerPos.x, playerPos.y, currentLevel] = TileType.Empty;
        Destroy(mapObjects[playerPos.x, playerPos.y, currentLevel]);
        PlaceGroundTile(playerPos);
        //limpiar variable y UI
        pendingTreasure = new Treasure();
        hudManager.HideTreasureMenu(); //despausar el juego
        hudManager.UpdateUHD(gameManager);
    }

    private void PlaceGroundTile(Vector2Int pos)
    {
        //asegurar que la posicion es valida antes de intentar acceder a mapObjects
        if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height) return;

        //instanciar el nuevo suelo
        GameObject ground = Instantiate(groundPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity, transform);

        //activarlo
        ground.SetActive(true);

        //reemplazar la referencia en la matriz de objetos
        mapObjects[pos.x, pos.y, currentLevel] = ground;
    }


    //funcion para ir revelando el area
    void RevealArea(Vector2Int centerPos)
    {
        //cuadrado alrededor del centro (playerPos)
        for (int x = centerPos.x - revealRadius; x <= centerPos.x + revealRadius; x++)
        {
            for (int y = centerPos.y - revealRadius; y <= centerPos.y + revealRadius; y++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    // obtener la ficha de niebla de esa casilla
                    GameObject fogTile = fogObjects[x, y, currentLevel];
                    
                    // si esta activa, la desactivamos 
                    if (fogTile != null && fogTile.activeSelf)
                    {
                        fogTile.SetActive(false); 
                    }
                }
            }
        }
    }
    
    //activar objetos del nivel y resetear los demas
    public void LoadLevel(int z, bool resetPlayerPosition = false)
    {
        //iterar todos los tiles y activar solo los del nivel y la niebla
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int k = 0; k < levels; k++)
                {
                    bool shouldBeActive = (k == z);

                    if (mapObjects[x, y, k] != null)
                        mapObjects[x, y, k].SetActive(shouldBeActive); 

                    // al cargar el nivel, hasta que RevealArea la apague
                    if (fogObjects[x, y, k] != null)
                    {
                        // prender la niebla solo si es el nivel actual
                        fogObjects[x, y, k].SetActive(shouldBeActive);
                    }
                }
            }
        }


        //resetear la posicion del jugador
        if (resetPlayerPosition)
        {
            playerPos = new Vector2Int(1, 1);
            playerObj.transform.position = new Vector3(playerPos.x, playerPos.y, 0);
        }

        //actualizar nivel e informar al gameManager
        currentLevel = z;
        gameManager.currentLevel = z;

        int treasureCount = treasuresPerLevel[z]; // obtener el total de tesoros guardado
        gameManager.maxTreasuresThisLevel = treasureCount;
        gameManager.treasuresRemaining = treasureCount; // resetear contador


        gameManager.ResetTimer(); //resetear timer

        //revelar area inicial
        RevealArea(playerPos);

        // actualizar HUD luego de cargar
        hudManager.UpdateUHD(gameManager);
    }


    //funcion para reiniciar despues de muerte    
    public void RestartGame()
    {
        Debug.Log("Reiniciando el juego...");

        //restaurar valores del GameManager
        gameManager.lives = 3;
        gameManager.maxEnergy = 4; //reiniciar energia a maximo nivel
        gameManager.energy = gameManager.maxEnergy;
        gameManager.currentScore = 0;
        gameManager.inventory.Clear(); //vaciar el inventario
        gameManager.ResetTimer(); //rsetear el timer a 100

        //reiniciar el Game Time
        Time.timeScale = 1f;

        GenerateAllLevels(); // Regenerar todos los niveles

        // Cargar el nivel 1 (índice 0)
        LoadLevel(0, true);

        //ocultar menu de Game Over
        if (hudManager.gameOverPanel != null)
        {
            hudManager.gameOverPanel.SetActive(false);
        }

        //actualizar HUD
        hudManager.UpdateUHD(gameManager);
    }

    //funcion para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }



    //funcion para cargar juego
    public void ApplyLoadedData(SaveData data)
    {
        Debug.Log($"Cargando datos. Nivel: {data.currentLevel}, Pos: ({data.playerX}, {data.playerY})");

        //destruir y Reinicializar matrices
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        width = data.width;
        height = data.height;
        levels = data.levels;

        mapData = new TileType[width, height, levels];
        mapObjects = new GameObject[width, height, levels];
        fogObjects = new GameObject[width, height, levels];
        treasureData = new Treasure[width, height, levels];

        //reconstruir logica y GameObjects visuales
        for (int z = 0; z < levels; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;

                    //reconstruir mapData y treasureData
                    mapData[x, y, z] = (TileType)data.mapDataList[z].tiles[index];
                    Treasure loadedTreasure = data.treasureDataList[z].treasures[index];
                    treasureData[x, y, z] = loadedTreasure;

                    Vector3 pos = new Vector3(x, y, 0);
                    GameObject prefabToUse = null;

                    switch (mapData[x, y, z])
                    {
                        case TileType.Wall: prefabToUse = wallPrefab; break;
                        case TileType.Trap: prefabToUse = trapPrefab; break;
                        case TileType.Exit: prefabToUse = exitPrefab; break;

                        case TileType.Treasure:
                            //recrear Tesoro
                            Instantiate(groundPrefab, pos, Quaternion.identity, transform);

                            GameObject treasureItem = Instantiate(treasurePrefab, pos, Quaternion.identity, transform);
                            if (treasureItem.TryGetComponent(out SpriteRenderer sr))
                            {
                                sr.sprite = loadedTreasure.sprite;
                            }
                            treasureItem.name = $"Treasure_{x}_{y}_L{z}";
                            treasureItem.SetActive(false);
                            mapObjects[x, y, z] = treasureItem;
                            break;

                        case TileType.Empty:
                        default:
                            prefabToUse = groundPrefab;
                            break;
                    }

                    //instanciar el prefab principal
                    if (mapData[x, y, z] != TileType.Treasure)
                    {
                        GameObject obj = Instantiate(prefabToUse, pos, Quaternion.identity, transform);
                        obj.name = $"Tile_{x}_{y}_L{z}";
                        obj.SetActive(false);
                        mapObjects[x, y, z] = obj;
                    }

                    //recrear Niebla
                    if (fogPrefab != null)
                    {
                        GameObject fogTile = Instantiate(fogPrefab, pos, Quaternion.identity, transform);
                        fogTile.name = $"Fog_{x}_{y}_L{z}";
                        fogTile.SetActive(false);
                        fogObjects[x, y, z] = fogTile;
                    }
                }
            }
        }

        currentLevel = data.currentLevel;
        playerPos = new Vector2Int(data.playerX, data.playerY);

        //cargar el nivel y colocar al jugador
        LoadLevel(currentLevel, false);
        playerObj.transform.position = new Vector3(playerPos.x, playerPos.y, 0);

        CameraManager camFollow = Camera.main.GetComponent<CameraManager>();
        if (camFollow != null)
        {
            camFollow.objetivo = playerObj.transform;
        }
    }


    public void SaveMapData(SaveData data)
    {
        data.mapDataList.Clear();
        data.treasureDataList.Clear();

        for (int z = 0; z < levels; z++)
        {
            TileRow tileRow = new TileRow();
            TreasureDataRow treasureDataRow = new TreasureDataRow();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //guardar el TileType
                    tileRow.tiles.Add((int)mapData[x, y, z]);

                    //guardamos los datos del tesoro en su struct
                    treasureDataRow.treasures.Add(treasureData[x, y, z]);
                }
            }
            data.mapDataList.Add(tileRow);
            data.treasureDataList.Add(treasureDataRow);
        }
    }
}