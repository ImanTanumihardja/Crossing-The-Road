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

    public Button selectButton;
    public RawImage lockImage;
   

    // Use this for initialization
    void Start () {
        GameObject jetpackManager = GameObject.Find("JetPackManager");
        pickJetpack = jetpackManager.GetComponent<PickJetPack>();


        jetpackList = pickJetpack.jetpackList;

        indexActive = 0;
        if (pickJetpack.SelectedJetpack != null)
        {
            // look for the selected jackpack to make active in the garage.
            for (int i = 0; i < jetpackList.Count; i++)
            {
                if (jetpackList[i].name == pickJetpack.SelectedJetpack.name)
                {
                    indexActive = i;
                    break;
                }
            }
        }
        // activate the selected jetpack
        jetpackList[indexActive].SetActive(true);
        ShowLock();
    }

    // Update is called once per frame
    void Update () {
        rotateJetpack();
	}

    void rotateJetpack()
    {
        foreach(GameObject o in jetpackList)
        {
            o.transform.Rotate(new Vector3(0, Time.deltaTime * 10, 0));
        }
    }

    public void NextJetPack()
    {
        jetpackList[indexActive].SetActive(false);
        indexActive = indexActive == jetpackList.Count - 1 ? 0 : indexActive + 1;
        jetpackList[indexActive].SetActive(true);

        // set locked or not here
        ShowLock();
    }

    public void PrevJetPack()
    {
        jetpackList[indexActive].SetActive(false);
        indexActive = indexActive == 0 ? jetpackList.Count - 1 : indexActive - 1;
        jetpackList[indexActive].SetActive(true);

        // set locked or not here
        ShowLock();
    }

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
        bool isLocked = pickJetpack.myJetpacks[name] == false;

        selectButton.enabled = !isLocked;
        lockImage.enabled = isLocked;
    }
}
