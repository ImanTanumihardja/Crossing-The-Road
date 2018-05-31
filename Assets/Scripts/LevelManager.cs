using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject[] Stages;
    public GameObject[] tilePrefabs;

    private GameObject player;
    private float spawnZ = 0.0f;
    public float tileLength = 72.0f;
    private int amnTilesOnScreen = 3;
    private float safeZone = 120f;
    private int lastPrefabIndex = 0;
    private float spawnZBack = 0.0f;
    private bool level = false;

    private List<GameObject> activeTiles;

    public Canvas frontWall;
    public Canvas backWall;

    public int score = 0;

    private int difficultyLevel = 0;
    private int maxDifficultyLevel = 100;
    private int scoreToNextLevel = 10;

    public Text scoreText;

    public Canvas gameController;
    public Canvas startButton;
    public Canvas garageButton;

    public List<GameObject> coinPrefabIndex;
    public List<GameObject> powerUpPrefabIndex;

    public ChickenMovement chickenMove;
    public CoinManager coinMan;

    private bool start = true;
    // Use this for initialization
    void Start () {
        activeTiles = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");

        for(int i = 0; i < amnTilesOnScreen; i++)
        {
            GameObject tile = null;
            if (i < 2)
            {
                tile = SpawnTile(0);
            }
            else
            {
                tile = SpawnTile();
            }
            SpawnCoins(tile);
            //SpawnPowerUp(tile);
        }
        backWall.GetComponent<Canvas>().enabled = false;
        gameController.GetComponent<Canvas>().enabled = false;
        chickenMove.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Space) && start){
            StartGame();
        }
        if(Input.GetKey(KeyCode.G)){
            OpenGarage();
        }
        GameObject tile = null;
		if(player.transform.position.z - safeZone >= (spawnZ - amnTilesOnScreen * tileLength))
        {
            tile = SpawnTile();
            DeleteTile();
            SpawnCoins(tile);
            SpawnPowerUp(tile);
        }

        if (score >= scoreToNextLevel)
        {
            LevelUp();
        }

        if (level)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Vehicle");

            foreach (GameObject go in gos)
            {
                CarMovement cm = go.GetComponent<CarMovement>();
                if (cm != null && cm.getLeveledUp())
                {
                    cm.SetSpeed(difficultyLevel);
                    cm.setLeveledUp(false);
                }
            }
        }


    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel += 10;
        difficultyLevel += 10;

        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            tilePrefabs[i] = Stages[1].transform.GetChild(i).gameObject;
        }

        level = true;

        Debug.Log("Level Up");
    }

    GameObject SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
            frontWall.transform.position = Vector3.forward * 216;
        }
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        //go = transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        frontWall.transform.position = Vector3.forward * spawnZ;
        activeTiles.Add (go);

        return go;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        spawnZBack += tileLength; 
        backWall.transform.position = Vector3.forward * spawnZBack;
        backWall.GetComponent<Canvas>().enabled = true;
       
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 0)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    public void ScoreUp()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public void SpawnCoins(GameObject parentTile)
    {
        GameObject go;
        // -1 means no cons prefab instantiated (blank)
        int randomIndex = Random.Range(-1, 2);
        if (randomIndex >= 0)
        {
            go = Instantiate(coinPrefabIndex[randomIndex]) as GameObject;
            go.transform.SetParent(parentTile.transform);

            float x = Random.Range(-50, 50);
            float y = Random.Range(-10, 15);
            float z = Random.Range(15, 50);
            Vector3 newPos = new Vector3(x, y, z);
            go.transform.localPosition = newPos;
        }
    }

    public void SpawnPowerUp(GameObject parentTile)
    {
        GameObject go;
        int randomIndex = Random.Range(-1, 3);
        if (randomIndex >= 0)
        {
            go = Instantiate(powerUpPrefabIndex[randomIndex]) as GameObject;
            go.transform.SetParent(parentTile.transform);

            float x = Random.Range(-50, 50);
            float y = Random.Range(-10, 15);
            float z = Random.Range(15, 50);
            Vector3 newPos = new Vector3(x, y, z);
            go.transform.localPosition = newPos;
        }
    }

    public void StartGame()
    {
        gameController.GetComponent<Canvas>().enabled = true;
        startButton.GetComponent<Canvas>().enabled = false;

        chickenMove.enabled = true;
        start = false;
    }

    public void OpenGarage()
    {
        SceneManager.LoadScene("Garage");
    }

    public void OpenRolling()
    {
        if(coinMan.CoinCount >= 100)
        {
            coinMan.CoinCount -= 100;
            coinMan.SaveCoins();
            SceneManager.LoadScene("Rolling");
        }
    }
}
