using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageManager : MonoBehaviour {

    private List<GameObject> jetpackList = null;
    private int indexActive;
    private PickJetPack pickJetpack;
    private bool isLocked = false;

    //Materials

    //Screws
    private int screwCount = 0;

    //Metal
    private int metalCount = 0;

    //Engine
    private int engineCount = 0;

    //Minigun
    private int minigunCount = 0;

    //Reactor Core
    private int reactorCoreCount = 0;

    public ScrollSnapRect srcoller;
    public List<Transform> placeHolders;
    public Transform jetPackHolder;
    public RectTransform container;
    public Button selectButton;
    public RawImage lockImage;

    public Text jetpackName;
    public Text screwText;
    public Text metalText;

    public GameObject recipeMenu;

    public Text jetPackNumScrewsText;
    public Text jetPackNumMetalText;
    public Text jetPackNumSpecialPartText;
    public GameObject jetPackPartsHolder;

    public Button buildButton;


    public Image speedImg;
    public Image fuelImg;

    //Parts
    public Dictionary<string, int> myJetPackParts = new Dictionary<string, int>();
    public List<GameObject> jetPackPartsList;


    void OnGUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Use this for initialization
    void Start () {

        LoadMyJetpackParts();

        screwCount = myJetPackParts["Screw"];
        metalCount = myJetPackParts["Metal"];
        engineCount = myJetPackParts["Engine"];
        minigunCount = myJetPackParts["Minigun"];
        reactorCoreCount = myJetPackParts["Reactor Core"];


        screwText.text = screwCount.ToString();
        metalText.text = metalCount.ToString();

        for (int i = 0; i < container.childCount; i++)
        {
            placeHolders.Add(container.GetChild(i));
        }

        GameObject jetpackManager = GameObject.Find("JetPackManager");
        pickJetpack = jetpackManager.GetComponent<PickJetPack>();

        jetpackList = pickJetpack.jetpackList;

        for(int i = 0; i < jetpackList.Count; i++)
        {
            for(int j = 0; j < placeHolders.Count; j++)
            {
                if (jetpackList[i].name == placeHolders[j].name)
                {
                    GameObject instance = Instantiate(jetpackList[i]) as GameObject;
                    instance.transform.position = placeHolders[j].transform.position;
                    instance.transform.rotation = new Quaternion(0, 180, 0, 0);
                    instance.transform.localScale = new Vector3(7, 7, 7);
                    instance.transform.parent = jetPackHolder;
                    instance.SetActive(true);
                }
            }
            
        }


        // make the SelectedJetpack indexactive.
        for (int i = 0; i < jetpackList.Count; i++)
        {
            if (jetpackList[i].name == pickJetpack.SelectedJetpack.name)
            {
                srcoller.SetPage(i);
                indexActive = i;
                break;
            }
        }
        //indexActive = 0;

        ShowLock();
        SetNumMaterials(indexActive);
        speedImg.fillAmount = jetpackList[indexActive].GetComponent<JetPack>().Power / 1.5f;
        fuelImg.fillAmount = jetpackList[indexActive].GetComponent<JetPack>().MaxFuel / 200.0f;
        jetpackName.text = jetpackList[indexActive].name.ToString();
    }

    // Update is called once per frame
    void Update () {
        //if(Input.GetKey(KeyCode.LeftArrow)){
        //    PrevJetPack();
        //}
        //if(Input.GetKey(KeyCode.RightArrow)){
        //    NextJetPack();
        //}
        //if(Input.GetKey(KeyCode.Space) && !isLocked){
        //    SelectJetPack();
        //}
        if(placeHolders[indexActive].transform.position != jetPackHolder.GetChild(indexActive).transform.position)
        {
            for(int i = 0; i < jetPackHolder.GetChildCount(); i++)
            {
                jetPackHolder.GetChild(i).transform.position = placeHolders[i].transform.position;
            }
        }

        screwText.text = screwCount.ToString();
        metalText.text = metalCount.ToString();

        if (indexActive != srcoller._currentPage)
        {
            indexActive = srcoller._currentPage;
            ShowLock();
            speedImg.fillAmount = jetpackList[indexActive].GetComponent<JetPack>().Power / 1.5f;
            fuelImg.fillAmount = jetpackList[indexActive].GetComponent<JetPack>().MaxFuel / 1000.0f;
            jetpackName.text = jetpackList[indexActive].name.ToString();
        }
        

        rotateJetpack(indexActive);
    }

    void SetNumMaterials(int currentPage)
    {
        jetPackNumScrewsText.text = jetpackList[currentPage].GetComponent<JetPack>().Screws.ToString();
        jetPackNumMetalText.text = jetpackList[currentPage].GetComponent<JetPack>().Metal.ToString();
        jetPackNumSpecialPartText.text = jetpackList[currentPage].GetComponent<JetPack>().SpecialPart;
        for (int i = 0; i < jetPackPartsHolder.transform.childCount; i++) 
        {
            if(jetPackPartsHolder.transform.GetChild(i).name == jetpackList[currentPage].GetComponent<JetPack>().SpecialPart)
            {
                jetPackPartsHolder.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                jetPackPartsHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    void rotateJetpack(int currentPage)
    {

            jetPackHolder.GetChild(currentPage).Rotate(new Vector3(0, Time.deltaTime * 10, 0));

    }

    //public void NextJetPack()
    //{
    //    jetpackList[indexActive].SetActive(false);
    //    indexActive = indexActive == jetpackList.Count - 1 ? 0 : indexActive + 1;
    //    jetpackList[indexActive].SetActive(true);
    //    jetpackList[indexActive].transform.position = target.transform.position;

    //    // set locked or not here
    //    ShowLock();
    //}

    //public void PrevJetPack()
    //{
    //    jetpackList[indexActive].SetActive(false);
    //    indexActive = indexActive == 0 ? jetpackList.Count - 1 : indexActive - 1;
    //    jetpackList[indexActive].SetActive(true);
    //    jetpackList[indexActive].transform.position = target.transform.position;

    //    // set locked or not here
    //    ShowLock();
    //}

    public void SelectJetPack()
    {
        pickJetpack.SelectedJetpack = jetpackList[indexActive];
        foreach (GameObject o in jetpackList)
        {
            o.SetActive(false);
        }
        pickJetpack.SaveMyJetpacks();
        SceneManager.LoadScene("Space");
    }

    /// <summary>
    /// Method to show the Lock image or not.
    /// </summary>
    void ShowLock()
    {
        string name = jetpackList[indexActive].name;
        isLocked = pickJetpack.myJetpacks[name] == false;

        if (!isLocked)
        {
            recipeMenu.SetActive(false);
            selectButton.enabled = true; //!isLocked;
            lockImage.enabled = false; //!isLocked;
        }
        else
        {
            recipeMenu.SetActive(true);
            SetNumMaterials(indexActive);
            ShowBuild();
            selectButton.enabled = false; //isLocked;
            lockImage.enabled = true; //isLocked;
        }

    }

    void ShowBuild()
    {
        if (screwCount >= jetpackList[indexActive].GetComponent<JetPack>().Screws && metalCount >= jetpackList[indexActive].GetComponent<JetPack>().Metal && myJetPackParts[jetpackList[indexActive].GetComponent<JetPack>().SpecialPart] >= 1) //add special parts check
        {
            buildButton.gameObject.SetActive(true);
        }
        else
        {
            buildButton.gameObject.SetActive(false);
        }
    }

    public void Build()
    {
        pickJetpack.myJetpacks[jetpackList[indexActive].gameObject.name] = true;
        ShowLock();
        if (jetpackList[indexActive].tag == "CommonJetPack")
        {
            screwCount -= 100;
            metalCount -= 100;
            
        }
        else if(jetpackList[indexActive].tag == "RareJetPack")
        {
            screwCount -= 200;
            metalCount -= 200;
        }
        else
        {
            screwCount -= 300;
            metalCount -= 300;
        }

        myJetPackParts[jetpackList[indexActive].GetComponent<JetPack>().SpecialPart]--;
        myJetPackParts["Screw"] = screwCount;
        myJetPackParts["Metal"] = metalCount;
        //add update for engieCount

        pickJetpack.SaveMyJetpacks();

        SaveJetPackParts();
        
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
    public void back()
    {
        if(pickJetpack.myJetpacks[jetpackList[indexActive].name] != false)
        {
            SelectJetPack();
        }
        else
        {
            indexActive = 0;
            SelectJetPack();
        }
        SceneManager.LoadScene("Space");
    }
}
