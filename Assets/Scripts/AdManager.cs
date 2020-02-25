using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public bool adResults = false;

    public PlayerManager playerManager;
    // Update is called once per frame
    void Update()
    {
        if (playerManager.LastAd == 10)
        {
            playerManager.LastAd = 0;
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
        }
    }

    public void playRewardedVideo()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }


    private void HandleShowResult(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            adResults = true;
        }
        else
        {
            Debug.Log("No award given. Result was :: " + result);
        }
    }
}
