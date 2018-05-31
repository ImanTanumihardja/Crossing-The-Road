using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickJetPack : MonoBehaviour
{
    public static PickJetPack instance;

    public List<GameObject> jetpackList;
    public GameObject SelectedJetpack;

    public Dictionary<string, bool> myJetpacks = new Dictionary<string, bool>();

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        // make the BasicJetpack selected by default.
        for (int i = 0; i < jetpackList.Count; i++)
        {
            if (jetpackList[i].name == "BasicJetpack")
            {
                SelectedJetpack = jetpackList[i];
                break;
            }
        }

        // load the myJetpacks Dictionary 
        LoadMyJetpacks();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    /// <summary>
    /// Method to save myJetpacks Dictionary to the PlayerPrefs store.
    /// </summary>
    public void SaveMyJetpacks()
    {
        string serializedString = "";
        foreach (string key in myJetpacks.Keys)
        {
            if (serializedString.Length == 0)
                serializedString = string.Format("{0}:{1}", key, myJetpacks[key]); // key + ":" + myJetpacks[key].ToString()
            else
                serializedString += string.Format("|{0}:{1}", key, myJetpacks[key]);
        }
        PlayerPrefs.SetString("myJetpacks", serializedString);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Method to load myJetpacks Dictionary either from PlayerPrefs if available,
    /// or initialize from the jetpackList.
    /// </summary>
    public void LoadMyJetpacks()
    {
        if (PlayerPrefs.HasKey("myJetpacks"))
        {
            myJetpacks.Clear();
            // The user has played this game before, and has a myJetpacks PlayerPrefs key.
            // So just load what was saved last time.
            string serializedString = PlayerPrefs.GetString("myJetpacks");
            string[] pairs = serializedString.Split('|');
            foreach (string pair in pairs)
            {
                string[] keyvalue = pair.Split(':');
                myJetpacks.Add(keyvalue[0], Convert.ToBoolean(keyvalue[1]));
            }
        }
        else
        {
            // In this case, this is the first time the player has played the game,
            // so there is no myJetpacks key in the PlayerPref. Now just populate 
            // the myJetpacks Dictionary with the names of all the jetpacks in the 
            // jetpackList and set their unlocked value to False except for the first/default one.
            myJetpacks.Clear();
            foreach (GameObject jp in this.jetpackList)
            {
                if (jp.name == "BasicJetpack")
                    myJetpacks.Add(jp.name, true);
                else
                    myJetpacks.Add(jp.name, false);
            }
        }
    }
}
