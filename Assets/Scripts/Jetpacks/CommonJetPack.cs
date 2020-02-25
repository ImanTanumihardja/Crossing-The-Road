using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonJetPack : JetPack
{
    private const float STEP = 25f;  // how far the chicken moves
    private const float POWER = 1.0f;  // power of jetpack
    private const float FUEL_ADD_FLIGHT = 0f;   // increment to fuel in flight
    private const float FUEL_MINUS = 50f;  // decriment to fuel
    private const float MAX_FUEL = 500f; // max fuel level of jetpack

    private const float NUM_SCREWS = 100F;
    private const float NUM_Metal = 100F;
    private const string SPECIAL_PART = "Engine";

    // NOTE: These variables are are inherited from the base class JetPack,
    // so you don't need to redeclare them in this class.

    //protected float fuelToAdd = FUEL_ADD;
    //protected Vector3 velocity = Vector3.zero;
    //protected ParticleSystem jetFlameLeft;
    //protected ParticleSystem jetFlameRight;
    //protected AudioSource jetNoise;
    //protected bool isJetpackOn = false;
    //[Range(0f, 100f)]
    //protected float fuel = 100f;

    /// <summary>
    /// Constructor for the JetPack class.
    /// </summary>
    public CommonJetPack()
    {
        // initialize the protected variables in the constructor.
        // These should be overriden in subclasses to initialize
        // appripriate values for other jetpack classes that inherit from this base class.
        step = CommonJetPack.STEP;
        power = CommonJetPack.POWER;
        fuel = CommonJetPack.MAX_FUEL;
        fuelToAdd = CommonJetPack.FUEL_ADD_FLIGHT;
        fuelToMinus = CommonJetPack.FUEL_MINUS;
        fuelToAddInFlight = CommonJetPack.FUEL_ADD_FLIGHT;
        maxFuel = CommonJetPack.MAX_FUEL;
        numScrews = CommonJetPack.NUM_SCREWS;
        numMetal = CommonJetPack.NUM_Metal;
        specialPart = CommonJetPack.SPECIAL_PART;
    }

    // Use this for initialization                            
    void Start()
    {
        ParticleSystem[] psArray = this.GetComponentsInChildren<ParticleSystem>();
        this.jetFlameRight = psArray[0];
        this.jetFlameLeft = psArray[1];

        this.jetNoise = this.GetComponentInChildren<AudioSource>();

        jetFlameLeft.Stop();
        jetFlameRight.Stop();
        jetNoise.Play();
        jetNoise.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isJetpackOn)
        {
            JetpackStart();
        }
        else
        {
            JetpackStop();
        }
    }
}
