using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {
    public float speed = 10;
    private float endPosition = -70;
    private Vector3 resetCar;
    // Use this for initialization
    void Start () {
        resetCar = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (gameObject.transform.position.x <= endPosition)
        {
            transform.position = resetCar;
        }
	}
}
