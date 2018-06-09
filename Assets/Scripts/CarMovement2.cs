using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement2 : MonoBehaviour
{
    public float speed = 10;
    public float endPosition = 90;
    private Vector3 resetCar;
    // Use this for initialization
    void Start()
    {
        resetCar = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (endPosition > 0)
        {
            if (gameObject.transform.position.x >= endPosition)
            {
                transform.position = resetCar;
            }
        }
        else
        {
            if (gameObject.transform.position.x <= endPosition)
            {
                transform.position = resetCar;
            }
        }
    }

    public void SetSpeed(float modifier)
    {
        speed = speed + modifier;
    }

   
}