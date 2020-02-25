using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private PlayerManager pm;
    private float originalSideStep;
    private bool isEngaged = false;

    private float maxTime = 5.0f;
    private float engagedTime = 0.0f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (this.engagedTime > 0.0f)
        {
            if (Time.time - this.engagedTime > maxTime)
            {
                Disenage();
            }
        }
    }

    /// <summary>
    /// Method to apply the powerup power to the player.
    /// </summary>
    public void Engage(PlayerManager playerManager)
    {
        if (!isEngaged)
        {
            this.isEngaged = true;
            this.engagedTime = Time.time;
            this.pm = playerManager;
            this.originalSideStep = this.pm.chickenMovement.JetPack.Power;
            this.pm.chickenMovement.JetPack.Power += 30.0f;

            // make the gameobject super small to hide it.
            this.gameObject.transform.localScale = Vector3.zero;
            this.gameObject.transform.position =  new Vector3(100, 0, 0);

            Debug.Log("PowerUp.Engage");
        }
    }

    public void Disenage()
    {
        this.pm.chickenMovement.JetPack.Power = this.originalSideStep;
        Debug.Log("PowerUp.Disengage");
        Destroy(this);
    }
}
