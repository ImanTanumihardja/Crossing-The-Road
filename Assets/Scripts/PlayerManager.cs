using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public ChickenMovement chickenMovement;
    public CoinManager coinManager;

    public Text LoseText;
    public AudioSource BackgroundMusic;
    public AudioSource LoseAudio;
    public Rigidbody playerRigidBody;


    // Use this for initialization
    void Start () {
        LoseAudio.Pause();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            Die();
        }
        if (other.gameObject.CompareTag("PowerUp"))
        {
            PowerUp pu = other.gameObject.GetComponent<PowerUp>();
            pu.Engage(this);
        }
    }

    public void Die()
    {
        // First save coins
        this.coinManager.SaveCoins();

        // Set timescale to stop game
        if (Time.timeScale == 1)
            Time.timeScale = 0.1f;
        else
            Time.timeScale = 1;

        BackgroundMusic.Stop();
        LoseAudio.UnPause();
        LoseText.gameObject.SetActive(true);

        StartCoroutine(WaitToReload());
    }

    /// <summary>
    /// Method to reset the scene.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(0.2f); SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void IncreaseSpeed()
    {
        chickenMovement.jetPack.SideStep += 50.0f;
        Debug.Log("IncresedSpeed");

    }
}


