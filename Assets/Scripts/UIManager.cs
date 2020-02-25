using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour {

    public PlayerManager playerManager;
    public AdManager adManager;

    public Button quitButton;
    public Button startButton;

    public SimpleCamera3D gyroCamera;
    public GameObject pauseMenu;
    public AudioSource music;
    public AudioListener audioListener;
    public Text coinText;
    public Text highScoreText;

    public float msToWait;
    public Text chestTimer;
    public Button coinsButton;

    private ulong lastFreeCoinOpen;
    private bool gameIsPaused = false;
    private bool isMusicOn = true;
    private bool is3DOn = true;
    private bool isSoundOn = true;
    // Use this for initialization
    void Start () {
		if(PlayerPrefs.GetInt("Music") == 1)
        {
            toggleMusic();
        }

        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            toggleSound();
        }

        is3DOn = gyroCamera.gyroEnabled;
        playerManager.LoadHighScore();
        highScoreText.text = ("!!! HighScore: " + playerManager.Highscore);
        coinText.text = playerManager.CoinCount.ToString();

        lastFreeCoinOpen = ulong.Parse(PlayerPrefs.GetString("LastFreeCoinOpen"));
        if (!IsChestReady())
            coinsButton.interactable = false;

    }
	
	// Update is called once per frame
	void Update () {
        coinText.text = playerManager.CoinCount.ToString();
        highScoreText.text = ("!!! HighScore: " + playerManager.Highscore.ToString());

        if (!coinsButton.IsInteractable())
        {
            if (IsChestReady())
            {
                coinsButton.interactable = true;
                return;
            }

            //Set the timer
            ulong diff = ((ulong)DateTime.Now.Ticks - lastFreeCoinOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = " ";
            //Hours
            r += ((int)secondsLeft / 3600).ToString() + "h";
            secondsLeft -= ((int)secondsLeft / 3600);
            //Minutes
            r += ((int)secondsLeft / 60).ToString("00") + "mins";
            //Seconds
            r += (secondsLeft % 60).ToString("00") + "s";

            chestTimer.text = r;
        }

        if (adManager.adResults)
        {
            lastFreeCoinOpen = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastFreeCoinOpen", DateTime.Now.Ticks.ToString());
            coinsButton.interactable = false;
            playerManager.CoinCount += 100;
            playerManager.SaveCoins();
            adManager.adResults = false;
        }

    }

    public bool Is3DOn
    {
        get
        {
            return is3DOn;
        }
    }

    public void freeCoinClick()
    {
        adManager.playRewardedVideo();

      

        //Where you would give gold to your player!
        //Roll a dice to see what reward the player gets
    }

    private bool IsChestReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastFreeCoinOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            chestTimer.text = "Free Coins";
            return true;
        }
        return false;
    }

    public void Pause()
    {
        if (gameIsPaused)
        {
            Debug.Log("UnPause");
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            gyroCamera.enabled = true;
            gameIsPaused = false;
        }
        else
        {
            Debug.Log("UnPause");
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            gyroCamera.enabled = false;
            gameIsPaused = true;
        }
    }

    public void toggleMusic()
    {
        if (isMusicOn)
        {
            music.gameObject.SetActive(false);
            isMusicOn = false;
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            music.gameObject.SetActive(true);
            isMusicOn = true;
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void toggle3D()
    {
        if (is3DOn)
        {
            //gyroCamera.gyroEnabled = false;
            is3DOn = false;
            PlayerPrefs.SetInt("3D", 1);
            gameIsPaused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            //gyroCamera.gyroEnabled = true;
            is3DOn = true;
            PlayerPrefs.SetInt("3D", 0);
            gameIsPaused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Restart()
    {
        Debug.Log("Restarting");
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void toggleSound()
    {
        if (isSoundOn)
        {
            AudioListener.pause = true;
            isSoundOn = false;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            AudioListener.pause = false;
            music.gameObject.SetActive(true);
            isSoundOn = true;
            PlayerPrefs.SetInt("Sound", 0);
        }
    }


}
