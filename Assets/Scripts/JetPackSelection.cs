using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackSelection : MonoBehaviour {

    private GameObject[] jetPackList;

    private void Start()
    {
        jetPackList = new GameObject[transform.childCount];
        //fill array with models
        for(int i = 0; i < transform.childCount; i++)
        {
            jetPackList[i] = transform.GetChild(i).gameObject;
        }

        //disable the model
        foreach(GameObject go in jetPackList)
        {
            go.SetActive(false);
        }

        //set active the first model
        if (jetPackList[0])
        {
            jetPackList[0].SetActive(true);
        }
    }

}
