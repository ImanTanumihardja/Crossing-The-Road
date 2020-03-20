using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rolling : MonoBehaviour {

    public Transform rollContainer;
    public AnimationCurve RollCurve;
    public GameObject target;
    public Text jetpackName;
    public Text rollingText;
    public Text prizeText;

    private PickJetPack pickJetPack;
    private Transform[] rolls;
    private const float ROLL_OFFSET = 300.0f;
    private bool isRolling = false;
    private float transition;
    private string giftName;
    private int jetpackNum;

    private string[] common = {"GreenJetPack", "YellowJetPack", "OrangeJetPack", "RedJetPack", "BlueJetPack", "WhiteJetPack", "PinkJetPack" };
    private string[] rare = { "BlackWhiteJetPack", "RainbowJetPack" };
    private string[] legendary = {"MachineGunJetPack",  "WingManJetPack", "SuperChickenCape"};

    private GameObject jetpackManager;
    private GameObject jp;

    //Parts
    private Dictionary<string, int> myJetPackParts = new Dictionary<string, int>();

    // Use this for initialization
    void Start () {


        LoadMyJetpackParts();
        jetpackManager = GameObject.Find("JetPackManager");
        pickJetPack = jetpackManager.GetComponent<PickJetPack>();
        rolls = new Transform[rollContainer.childCount];
		for(int i = 0; i < rollContainer.childCount; i++)
        {
            rolls[i] = rollContainer.GetChild(i);
        }



    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.R)){
            chestClick();
        }
        if (isRolling)
        {
            Vector3 end = (-Vector3.right * ROLL_OFFSET) * (rolls.Length - 1);
            rollContainer.transform.localPosition = Vector3.Lerp(Vector3.right * ROLL_OFFSET, end, RollCurve.Evaluate(transition));
            transition += Time.deltaTime / 2.0f;
            if(transition > 1)
            {
                isRolling = false;
                int randNum = Random.Range(0, 3);
                switch (giftName)
                {
                    
                    case "Small":
                       
                        if (randNum == 0)
                        {
                            PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + 50);
                            prizeText.text = ("+50 Coins");
                        }
                        else if (randNum == 1)
                        {
                            myJetPackParts["Screw"] = myJetPackParts["Screw"] + 50;
                            prizeText.text = ("+50 Screws");
                        }
                        else
                        {
                            myJetPackParts["Metal"] = myJetPackParts["Metal"] + 50;
                            prizeText.text = ("+50 Metal");
                        }
                        SaveJetPackParts();
                        PlayerPrefs.Save();
                        Debug.Log("You got a Common");
                        break;

                    case "Large":
                        
                        if (randNum == 0)
                        {
                            PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + 100);
                            prizeText.text = ("+100 Coins");
                        }
                        else if (randNum == 1)
                        {
                            myJetPackParts["Screws"] = myJetPackParts["Screws"] + 100;
                            prizeText.text = ("+100 Screws");
                        }
                        else
                        {
                            myJetPackParts["Metal"] = myJetPackParts["Metal"] + 100;
                            prizeText.text = ("+100 Metal");
                        }
                        SaveJetPackParts();
                        PlayerPrefs.Save();
                        Debug.Log("You got a Rare");
                        break;

                    case "JetPack":

                        jetPackRoll();
                        break;
                }


                SceneManager.LoadScene("Space");
            }
        }
	}

    public void chestClick()
    {
        if (!isRolling)
        {
            StartCoroutine(changeColor());
            transition = 0;
            isRolling = true;
            float offset = 0.0f;
            List<int> indexes = new List<int>();
            for (int i = 0; i < rolls.Length; i++)
            {
                indexes.Add(i);
            }
            for (int i = 0; i < rolls.Length; i++)
            {
                int index = indexes[UnityEngine.Random.Range(0, indexes.Count)];
                indexes.Remove(index);
                rolls[index].transform.localPosition = Vector2.right * offset;
                offset += ROLL_OFFSET;
                giftName = rolls[index].name;
            }
        }
           
    }

    private void jetPackRoll()
    {
        int num = Random.Range(0, 101);
        if(num == 1)
        {
            jetpackNum = Random.Range(0, legendary.Length);
            for (int i = 0; i <= pickJetPack.jetpackList.Count; i++)
            {
                if (pickJetPack.jetpackList[i].name == legendary[jetpackNum])
                {
                    jp = Instantiate(pickJetPack.jetpackList[i]);
                    jp.SetActive(true);
                    jp.transform.position = target.transform.position;
                    jetpackName.text = pickJetPack.jetpackList[i].name.ToString();
                    break;
                }
            }
            pickJetPack.myJetpacks[legendary[jetpackNum]] = true;
            Debug.Log("You got a Legendary");
        }
        else if (num >= 75)
        {
            jetpackNum = Random.Range(0, rare.Length);
            for (int i = 0; i <= pickJetPack.jetpackList.Count; i++)
            {
                if (pickJetPack.jetpackList[i].name == rare[jetpackNum])
                {
                    jp = Instantiate(pickJetPack.jetpackList[i]);
                    jp.SetActive(true);
                    jp.transform.position = target.transform.position;
                    jetpackName.text = pickJetPack.jetpackList[i].name.ToString();
                    break;
                }

            }
            pickJetPack.myJetpacks[rare[jetpackNum]] = true;
            Debug.Log("You got a Rare");
        }
        else
        {
            jetpackNum = Random.Range(0, common.Length);
            for (int i = 0; i <= pickJetPack.jetpackList.Count; i++)
            {
                if (pickJetPack.jetpackList[i].name == common[jetpackNum])
                {
                    jp = Instantiate(pickJetPack.jetpackList[i]);
                    jp.SetActive(true);
                    jp.transform.position = target.transform.position;
                    jetpackName.text = pickJetPack.jetpackList[i].name.ToString();
                    break;
                }

            }
            pickJetPack.myJetpacks[common[jetpackNum]] = true;
            Debug.Log("You got a Common");
        }    

        pickJetPack.SaveMyJetpacks();
    }

    private IEnumerator changeColor()
    {
        float elapsedTime = 0;

        while (elapsedTime < 10.0f)
        {
            rollingText.color = Random.ColorHSV();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
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

}
