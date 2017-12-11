using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private PlayerManager pm;
    private float originalStep;

    public float maxTime = 1f;
    public float engagedTime = -1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (this.engagedTime > 0)
            if (Time.time - this.engagedTime > maxTime)
            {
                Disenage();
            }
    }

    /// <summary>
    /// Method to apply the powerup power to the player.
    /// </summary>
    public void Engage(PlayerManager playerManager)
    {
        this.engagedTime = Time.time;
        this.pm = playerManager;
        this.originalStep = this.pm.chickenMovement.jetPack.SideStep;
        this.pm.chickenMovement.jetPack.SideStep = 50.0f;

        // make the gameobject super small to hide it.
        this.gameObject.transform.localScale = Vector3.zero;

        Debug.Log("PowerUp.Engage");
    }

    public void Disenage()
    {
        this.pm.chickenMovement.jetPack.SideStep = this.originalStep;
        Debug.Log("PowerUp.Disengage");
        this.enabled = false;
    }



}
