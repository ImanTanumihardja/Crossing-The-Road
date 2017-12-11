using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Range(0f, 100f)]
    protected float _fuel = 100f;
    public const float MAX_FUEL = 100f;

    public float maxTime = 10f;
    public float currentTime;

    void Start()
    {
      
    }

    public float fuel
    {
        get
        {
            return _fuel;
        }
        set
        {
            _fuel = Mathf.Min(MAX_FUEL, value);
        }
    }

    void Update()
    {
        //currentTime = currentTime + Time.deltaTime;
        //if (currentTime >= maxTime)
        //{
        //    fuel = 100;
        //    currentTime = 0f;
        //}
    }
}
