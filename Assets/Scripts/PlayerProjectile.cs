using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    public GameObject spawnPosObj;
    public GameObject bullet;
    public GameObject spawnPosObj2;

    public float maxTime = 10f;
    public float currentTime;

    // Use this for initialization
    void Start () {

   
}
	
	// Update is called once per frame
	void Update () {
        currentTime = currentTime + Time.deltaTime;
        if (currentTime >= maxTime)
        {
            Instantiate(bullet, spawnPosObj.transform.position, bullet.transform.rotation);
            

            Instantiate(bullet, spawnPosObj2.transform.position, bullet.transform.rotation);
            currentTime = 0f;
        }
        
	}
}
