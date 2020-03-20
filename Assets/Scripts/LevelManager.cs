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
    private int amnTilesOnScreen = 4;
    private float safeZone = 120f;
    private int lastPrefabIndex = 0;
    private float spawnZBack = 0.0f;
    private bool isLevelUp = false;
    private int lastDeleteTile = 0;
    private int lastChangeStage = 0;

    private List<GameObject> activeTiles;
   

    public Canvas frontWall;
    public Canvas backWall;
    public GameObject startBack;
    public GameObject barrier;

    public GameObject leftBackSide;
    public GameObject rightBackSide;

    public GameObject[] stage1Front;
    public GameObject[] stage2Front;
    public GameObject[] stage3Front;


    public int score = 0;
    private int lastScore = 0;

    private int difficultyLevel = 0;
    private int maxDifficultyLevel = 50;
    private int scoreToNextLevel = 10;

    private int stageNum = 0;
    private int lastStageNum = 0;

    public Text scoreText;

    public Canvas gameController;
    public Canvas startButton;
    public Canvas garageButton;

    public GameObject coinPrefab;
    public GameObject powerUp;

    //Managers
    public ChickenMovement chickenMove;
    public PlayerManager playerMan;
    public UIManager uiMan;

    public GameObject jetpackManager;

    private PickJetPack pickJetpack;

    private bool start = true;
    private bool isSwitching = false;
    private bool hasMoved = false;

    private int j = 8;

    private int c = 0;

    private bool isCursorVisable = true;
    private CursorLockMode isCursorLocked = CursorLockMode.None;

    public SimpleCamera3D gyroCamera;

    public GameObject pauseMenu;
    private bool gameIsPaused = false;    

    // Use this for initialization

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void OnGUI()
    {
        Cursor.lockState = isCursorLocked;
        Cursor.visible = isCursorVisable;
    }

    void Start () {

        jetpackManager = GameObject.Find("JetPackManager");
        pickJetpack = jetpackManager.GetComponent<PickJetPack>();
        foreach(GameObject jetpack in pickJetpack.jetpackList)
        {
            jetpack.transform.position = new Vector3(0, 0, -72);
        }


        activeTiles = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");

        for(int i = 0; i < amnTilesOnScreen; i++)
        {
            GameObject tile = null;
            if (i < 1)
            {
                tile = SpawnTile(0);
            }
            else
            {
                tile = SpawnTile();
            }
        }
        frontWall.gameObject.SetActive(true);
        backWall.gameObject.SetActive(false);
        gameController.GetComponent<Canvas>().enabled = false;
        chickenMove.enabled = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Space) && start){
            StartGame();
        }
        if(Input.GetKey(KeyCode.G) && start){
            OpenGarage();
        }
        if(Input.GetKey(KeyCode.R) && start)
        {
            OpenRolling();
        }
        GameObject tile = null;
		if(player.transform.position.z - safeZone >= (spawnZ - amnTilesOnScreen * tileLength))
        {
            tile = SpawnTile();
            SpawnCoins(tile);
            SpawnPowerUp(tile);
            SpawnMaterials(tile);
            if(score > 2 && !uiMan.Is3DOn)
            {
                DeleteTile();
            }
        }

        if(score - lastDeleteTile >= 2 && score <= 2 && !uiMan.Is3DOn)
        {
            DeleteTile();
            lastDeleteTile = score;
        }


        if (score == scoreToNextLevel)
        {
            LevelUp();
        }

        if (isLevelUp)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Vehicle");

            foreach (GameObject go in gos)
            {
                CarMovement cm = go.GetComponent<CarMovement>();
                if (cm != null && !cm.getLeveledUp())
                {
                    cm.AddSpeed(difficultyLevel);
                    cm.setLeveledUp(true);
                }
            }
        }

        // The player has moved if the score is greater than lastscore
        hasMoved = (score > lastScore);

        //forntwall
        if ((score % 10 == 3) && !isSwitching) // && score - lastChangeStage >= 10)
        {
            lastChangeStage = score;
            lastStageNum = stageNum;
            //stageNum = stageNum == 0 ? 1 : 0;
            stageNum = Random.Range(0, 3);
            isSwitching = true;
        }

        if (score > 0 && score % 10 == 0 && isSwitching) //&& score == lastChangeStage + 7)
        {

            isSwitching = false;
            // reset j,k for front walls
            j = 8;

            // reset c,u for back walls
            c = 0;
        }

        if (hasMoved && isSwitching)
        {
            switchFrontWalls();

        }

        //backwall
        if (score >= 2)
        {
            backWall.gameObject.SetActive(true);
        }

        if (score > 10 && score % 10 >= 3 && hasMoved)
        {
           // switchBackWalls();
        }

        //if (score >= 19)
        //{
        //    startBack.SetActive(false);
        //}

        //barrier
        if (hasMoved)
        {
            lastScore = score;
            Debug.Log("Barrier Moved");
        }

        if(score > playerMan.Highscore)
        {
            playerMan.SaveHighScore(score);
        }
    }

    public void moveBarrier(Collider other)
    {
        barrier.transform.position = new Vector3(0, 0, other.transform.position.z - 2);
    }

    void switchFrontWalls()
    {

        if (stageNum == lastStageNum)
        {
            return;
        }
        else if (stageNum == 0)
        {
            stage1Front[j].SetActive(true);
            stage2Front[j].SetActive(false);
            stage3Front[j].SetActive(false);

            j--;
        }
        else if (stageNum == 1)
        {
            stage2Front[j].SetActive(true);
            stage1Front[j].SetActive(false);
            stage3Front[j].SetActive(false);
            j--;
        }else if(stageNum == 2)
        {
            stage3Front[j].SetActive(true);
            stage2Front[j].SetActive(false);
            stage1Front[j].SetActive(false);
            j--;   
        }
    }

    void switchBackWalls()
    {
        
        if (lastStageNum == 0)
        {

            c++;
        }
        else if (lastStageNum == 1)
        {

            c++;
        }
    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }
            

        scoreToNextLevel += 10;
        difficultyLevel += 5;

        GameObject Stage = Stages[stageNum];

        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            tilePrefabs[i] = Stage.transform.GetChild(i).gameObject;
        }

        isLevelUp = true;

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
        if (randomIndex >= 1)
        {
            float x = Random.Range(-50, 50);
            float y = Random.Range(20, 40);
            float z = Random.Range(15, 50);
            for (int i = 0; i < Random.Range(1, 6); i++)
            {
                go = Instantiate(coinPrefab) as GameObject;
                go.transform.SetParent(parentTile.transform);


                Vector3 newPos = new Vector3(x, y, z);
                go.transform.localPosition = newPos;
                z -= 5;
            }
           
        }
    }

    public void SpawnPowerUp(GameObject parentTile)
    {
        GameObject go;
        int randomIndex = Random.Range(0, 6);
        if (randomIndex == 4)
        {
            go = Instantiate(powerUp) as GameObject;
            go.transform.SetParent(parentTile.transform);

            float x = Random.Range(-30, 30);
            float y = Random.Range(20, 40);
            float z = Random.Range(15, 50);
            Vector3 newPos = new Vector3(x, y, z);
            go.transform.localPosition = newPos;
        }
    }

    public void SpawnMaterials(GameObject parentTile)
    {
        GameObject go;
        int randomIndex = Random.Range(0, 50);
        if(randomIndex == 0)
        {
            go = Instantiate(playerMan.jetPackPartsList[Random.Range(3, 6)]) as GameObject;
            go.transform.SetParent(parentTile.transform);

            float x = Random.Range(-30, 30);
            float y = Random.Range(20, 40);
            float z = Random.Range(15, 50);
            Vector3 newPos = new Vector3(x, y, z);
            go.transform.localPosition = newPos;
        }
        else if(randomIndex <= 5)
        {
            go = Instantiate(playerMan.jetPackPartsList[Random.Range(2, 3)]) as GameObject;
            go.transform.SetParent(parentTile.transform);

            float x = Random.Range(-30, 30);
            float y = Random.Range(20, 40);
            float z = Random.Range(15, 50);
            Vector3 newPos = new Vector3(x, y, z);
            go.transform.localPosition = newPos;
        }
        else if (randomIndex <= 40)
        {
            float x = Random.Range(-30, 30);
            float y = Random.Range(20, 40);
            float z = Random.Range(15, 50);
            int matType = Random.Range(0, 2);

            for (int i = 0; i < Random.Range(1, 6); i++)
            {
                go = Instantiate(playerMan.jetPackPartsList[matType]) as GameObject;
                go.transform.SetParent(parentTile.transform);


                Vector3 newPos = new Vector3(x, y, z);
                go.transform.localPosition = newPos;
                z -= 5;
            }
        }

    }

    public void StartGame()
    {

        //isCursorVisable = false;
        //isCursorLocked = CursorLockMode.Locked;

        gameController.GetComponent<Canvas>().enabled = true;
        startButton.GetComponent<Canvas>().enabled = false;

        chickenMove.enabled = true;
        start = false;
    }

    public void OpenGarage()
    {

        playerMan.SaveCoins();
        SceneManager.LoadScene("Garage");
    }

    public void OpenRolling()
    {
        if(playerMan.CoinCount >= 100)
        {
            playerMan.CoinCount -= 100;
            playerMan.SaveCoins();
            SceneManager.LoadScene("Rolling");
        }
    }



   

}
