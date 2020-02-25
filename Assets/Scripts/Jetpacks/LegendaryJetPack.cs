using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryJetPack : JetPack
{
    private const float STEP = 25f;  // how far the chicken moves
    private const float POWER = 1.5f;  // power of jetpack
    private const float FUEL_ADD_FLIGHT = 0;   // increment to fuel in flight
    private const float FUEL_MINUS = 50f;  // decriment to fuel
    private const float MAX_FUEL = 1000f; // max fuel level of jetpack

    private const float NUM_SCREWS = 300F;
    private const float NUM_Metal = 300F;
    private const string SPECIAL_PART = "None";

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
    public LegendaryJetPack()
    {
        // initialize the protected variables in the constructor.
        // These should be overriden in subclasses to initialize
        // appripriate values for other jetpack classes that inherit from this base class.
        step = LegendaryJetPack.STEP;
        power = LegendaryJetPack.POWER;
        fuel = LegendaryJetPack.MAX_FUEL;
        fuelToAdd = LegendaryJetPack.FUEL_ADD_FLIGHT;
        fuelToMinus = LegendaryJetPack.FUEL_MINUS;
        fuelToAddInFlight = LegendaryJetPack.FUEL_ADD_FLIGHT;
        maxFuel = LegendaryJetPack.MAX_FUEL;
        numScrews = LegendaryJetPack.NUM_SCREWS;
        numMetal = LegendaryJetPack.NUM_Metal;
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
