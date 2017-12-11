using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {
    public int coinsToGive = 1;
    public Text coinText;
    public AudioSource coinNoise;
    public bool isCoinsLoaded = false;


    private int coinCount = 0;

    // Use this for initialization
    void Start () {
        LoadCoins();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// CoinCount property
    /// </summary>
    public int CoinCount
    {
        get
        {
            return coinCount;
        }
        set
        {
            coinCount = value;
            if (coinText != null) coinText.text = coinCount.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            CoinCount += 1;
            other.gameObject.SetActive(false);
            coinNoise.Play();
            
        }  
    }

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
}
