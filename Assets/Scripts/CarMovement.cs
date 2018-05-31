using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {
    public float speed = 10;
    public float endPosition = -70;
    private Vector3 resetCar;
    private bool leveledUp = true;

    // Use this for initialization
    void Start () {
        resetCar = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if(endPosition < 0)
        {
            if (gameObject.transform.position.x <= endPosition)
            {
                transform.position = resetCar;
            }
        }
      
        else
        {
            if (gameObject.transform.position.x >= endPosition)
            {
                transform.position = resetCar;
            }
        }
        
	}

    public void SetSpeed(float modifier)
    {
        speed += modifier;
    }

    public bool getLeveledUp()
    {
        return leveledUp;
    }
    public void setLeveledUp(bool input)
    {
        leveledUp = input;
    }
}
