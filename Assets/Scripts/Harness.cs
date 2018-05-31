using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harness : MonoBehaviour {

    public GameObject[] JetPackList;
    public JetPack JetPack;

    private void Start()
    {
        JetPackList = new GameObject[transform.childCount];
        //fill array with models
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject jp = transform.GetChild(i).gameObject;
            JetPackList[i] = jp;

            // make the default jetpack the BasicJetpack.
            if (jp.name == "BasicJetpack")
                JetPack = jp.GetComponent<JetPack>();
        }

        // When we get back to the Space scene,
        // make sure to load the selected Jetpack
        // the user selected in the Garage scene.
        LoadJetpack();
    }

    void LoadJetpack()
    {
        GameObject jetpackManager = GameObject.Find("JetPackManager");
        PickJetPack pickJetPack = jetpackManager.GetComponent<PickJetPack>();
        GameObject selectedJetpack = pickJetPack.SelectedJetpack;
        if (selectedJetpack == null) return;

        foreach(GameObject jp in JetPackList)
        {
            if (jp.name == selectedJetpack.transform.name)
            {
                jp.SetActive(true);
                JetPack = jp.GetComponent<JetPack>();
            }
            else
            {
                jp.SetActive(false);
            }
        }
    }

}
