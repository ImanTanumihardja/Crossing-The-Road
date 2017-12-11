using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShipMovement : MonoBehaviour {
    public float speed = 10;
    private float endPosition = 120;
    private Vector3 resetCar;
    // Use this for initialization
    void Start () {
        resetCar = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (gameObject.transform.position.x >= endPosition)
        {
            transform.position = resetCar;
        }
    }
}
