using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public ChickenMovement chickenMovement;

    public Text LoseText;
    public AudioSource BackgroundMusic;
    public AudioSource LoseAudio;
    public Rigidbody playerRigidBody;
    public GameObject Explosion;
    public GameObject DrumStick;
    public GameObject Chicken;
    public GameObject pickupEffect;
    public bool InvincibleMode = false;
    public List<GameObject> jetPackPartsList;

    //Highscore
    private int highscore = 0;

    //Coins
    public AudioSource coinNoise;
    public bool isCoinsLoaded = false;

    private int coinCount = 0;

    //Materials
    public AudioSource materialPickUpAudio;

    //Screws
    private int screwCount = 0;

    //Metal
    private int metalCount = 0;

    //Engines
    private int engineCount = 0;

    //Minigun
    private int minigunCount = 0;

    //Reactor Core
    private int reactorCoreCount = 0;

    //Cryptonite
    private int cryptoniteCount = 0;

    //Parts
    public Dictionary<string, int> myJetPackParts = new Dictionary<string, int>();

    private bool died = false;
    private int lastAd = 0;
    private float timeLeft = 150.0f;
    private float timeDie = 0.0f;

    private float originalSideStep;
    private bool isEngaged = false;

    private float maxTime = 5.0f;
    private float engagedTime = 0.0f;

    private Transform puTransform;


    // Use this for initialization
    void Start ()
    {
        LoadMyJetpackParts();

        screwCount = myJetPackParts["Screw"];
        metalCount = myJetPackParts["Metal"];
        engineCount = myJetPackParts["Engine"];
        minigunCount = myJetPackParts["Minigun"];
        reactorCoreCount = myJetPackParts["Reactor Core"];
        cryptoniteCount = myJetPackParts["Cryptonite"];

        LoadCoins();
        LoadHighScore();
        LoseAudio.Pause();
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (SceneManager.GetActiveScene().name != "Space")
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (died && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1)))
        {
            died = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
        if (died)
        {
            timeLeft -= (Time.time - timeDie);
            if (timeLeft <= 0)
            {
                died = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
            }
            
        }

        //if (this.engagedTime > 0.0f)
        //{
        //    if (Time.time - this.engagedTime > maxTime)
        //    {
        //        Disenage();
        //    }
        //}
    }

    public int LastAd
    {
        get
        {
            return lastAd;
        }
        set
        {
            lastAd = value;
        }
    }


    public int Highscore
    {
        get
        {
            return highscore;
        }
    }

    public int CoinCount
    {
        get
        {
            return coinCount;
        }
        set
        {
            coinCount = value;
        }
    }

    public int ScrewCount
    {
        get
        {
            return screwCount;
        }
    }

    public int MetalCount
    {
        get
        {
            return metalCount;
        }
    }

    public int EngineCount
    {
        get
        {
            return engineCount;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            Die();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            puTransform = other.gameObject.GetComponent<Transform>();
            Destroy(other.gameObject);

            // cool pick up effect
            GameObject effect = Instantiate(pickupEffect, puTransform.position, puTransform.rotation);
            Destroy(effect, 1);

            EngagePowerUp();

            
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            coinCount += 1;
            other.gameObject.SetActive(false);
            coinNoise.Play();
            SaveCoins();

        }

        if (other.gameObject.CompareTag("Material"))
        {
            if (other.gameObject.name == "Screw(Clone)")
            {
                screwCount++;
                myJetPackParts["Screw"] = screwCount;
            }

            else if(other.gameObject.name == "Metal(Clone)")
            {
                metalCount++;
                myJetPackParts["Metal"] = metalCount;
            }
            else if(other.gameObject.name == "Engine(Clone)")
            {
                engineCount++;
                myJetPackParts["Engine"] = engineCount;
            }
            else if(other.gameObject.name == "Minigun(Clone)")
            {
                minigunCount++;
                myJetPackParts["Minigun"] = minigunCount;
            }
            else if(other.gameObject.name == "Reactor Core(Clone)")
            {
                reactorCoreCount++;
                myJetPackParts["Reactor Core"] = reactorCoreCount;
            }
            else if(other.gameObject.name == "Cryptonite(Clone)")
            {
                cryptoniteCount++;
                myJetPackParts["Cryptonite"] = cryptoniteCount;
            }

            SaveJetPackParts();
            other.gameObject.SetActive(false);
            materialPickUpAudio.Play();
        }
    }

    void Die()
    {
        if (InvincibleMode) return;

        lastAd++;


        Explosion.SetActive(true);
        Chicken.SetActive(false);
        DrumStick.SetActive(true);


        // Set timescale to stop game
        //if (Time.timeScale == 1)
            Time.timeScale = 0.1f;
        //else
        //    Time.timeScale = 1;

       
        BackgroundMusic.Stop();
        LoseAudio.Play();
        LoseText.gameObject.SetActive(true);
        chickenMovement.enabled = false;
        timeDie = Time.time;
        died = true;
    }

    public void EngagePowerUp()
    {
        //if (!isEngaged)
        //{
        //    this.isEngaged = true;
        //    this.engagedTime = Time.time;
            //this.originalSideStep = this.chickenMovement.JetPack.Power;
            this.chickenMovement.JetPack.JetpackFull();

            // make the gameobject super small to hide it.
            //pu.transform.localScale = Vector3.zero;
            //pu.transform.position = new Vector3(100, 0, 0);

            
            Debug.Log("PowerUp.Engage");
        //}
    }

    public void Disenage()
    {
        //this.chickenMovement.JetPack.Power = this.originalSideStep;
        Debug.Log("PowerUp.Disengage");
        Destroy(this);
    }


    //PlayerPrefs


    //Coins
    public void SaveCoins()
    {
        Debug.Log("SaveCoins:" + coinCount);
        PlayerPrefs.SetInt("PlayerCoins", coinCount);
        PlayerPrefs.Save();
    }

    public void LoadCoins()
    {
        if (!this.isCoinsLoaded)
        {
            CoinCount = PlayerPrefs.GetInt("PlayerCoins");
            Debug.Log("LoadCoins:" + coinCount);
        }
    }

    //HighScore
    public void SaveHighScore(int score)
    {
        Debug.Log("Saved HighScore");
        PlayerPrefs.SetInt("HighScore", score);
    }

    public void LoadHighScore()
    {
        Debug.Log("Load HighScore");
        highscore = PlayerPrefs.GetInt("HighScore");
    }

    public void SaveJetPackParts()
    {
        string serializedString = "";
        foreach (string key in myJetPackParts.Keys)
        {
            if (serializedString.Length == 0)
                serializedString = string.Format("{0}:{1}", key, myJetPackParts[key]); // key + ":" + myJetpacks[key].ToString()
            else
                serializedString += string.Format("|{0}:{1}", key, myJetPackParts[key]);
        }
        PlayerPrefs.SetString("myJetPackParts", serializedString);
        PlayerPrefs.Save();
    }

    public void LoadMyJetpackParts()
    {
        if (PlayerPrefs.HasKey("myJetPackParts"))
        {
            myJetPackParts.Clear();
            // The user has played this game before, and has a myJetpacks parts PlayerPrefs key.
            // So just load what was saved last time.
            string serializedString = PlayerPrefs.GetString("myJetPackParts");
            string[] pairs = serializedString.Split('|');
            foreach (string pair in pairs)
            {
                string[] keyvalue = pair.Split(':');
                myJetPackParts.Add(keyvalue[0], int.Parse(keyvalue[1]));
            }
        }
        else
        {
            // In this case, this is the first time the player has played the game,
            // so there is no myJetpacks key in the PlayerPref. Now just populate 
            // the myJetpacksParts Dictionary with the names of all the jetpacksparts 
            myJetPackParts.Clear();
            foreach (GameObject jp in this.jetPackPartsList)
            {
                    myJetPackParts.Add(jp.name, 0);
            }
        }
    }


}


