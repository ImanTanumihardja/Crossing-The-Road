using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Button quitButton;
    public Button startButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            quitButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
        }
    else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            quitButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
}
