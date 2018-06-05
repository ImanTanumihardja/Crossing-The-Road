 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FreeCoins : MonoBehaviour
{
    public CoinManager coinManager;

    public float msToWait = 43200000.0f;

    public Text chestTimer;
    public Button coinsButton;
    private ulong lastFreeCoinOpen;

    //public PickUpPoints coinCount;

    private void Start()
    {
        lastFreeCoinOpen = ulong.Parse(PlayerPrefs.GetString("LastFreeCoinOpen"));
        if (!IsChestReady())
            coinsButton.interactable = false;
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.C)){
            freeCoinClick();
        }
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
    }

    public void freeCoinClick()
    {
        lastFreeCoinOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastFreeCoinOpen", DateTime.Now.Ticks.ToString());
        coinsButton.interactable = false;
        coinManager.CoinCount += 80;

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
}

