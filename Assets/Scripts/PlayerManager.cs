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
    public GameObject Explosion;
    public GameObject DrumStick;
    public GameObject Chicken;

    private bool died = false;


    // Use this for initialization
    void Start () {
        LoseAudio.Pause();
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (died && (Input.GetKey(KeyCode.Space) || (Input.touchCount == 1)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            died = false;
        }
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

        Explosion.SetActive(true);
        Chicken.SetActive(false);
        DrumStick.SetActive(true);

        // Set timescale to stop game
        if (Time.timeScale == 1)
            Time.timeScale = 0.1f;
        else
            Time.timeScale = 1;

       
        BackgroundMusic.Stop();
        LoseAudio.UnPause();
        LoseText.gameObject.SetActive(true);
        died = true;
       
    }

}


