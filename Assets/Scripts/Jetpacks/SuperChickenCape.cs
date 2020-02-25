using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperChickenCape : JetPack
{
    private const float STEP = 25f;  // how far the chicken moves
    private const float POWER = 1.5f;  // power of jetpack
    private const float FUEL_ADD_FLIGHT = 0f;   // increment to fuel in flight
    private const float FUEL_MINUS = 50f;  // decriment to fuel
    private const float MAX_FUEL = 1000f; // max fuel level of jetpack

    private const float NUM_SCREWS = 300F;
    private const float NUM_Metal = 300F;
    private const string SPECIAL_PART = "Cryptonite";

    protected ParticleSystem rightSmoke;
    protected ParticleSystem leftSmoke;

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
    public SuperChickenCape()
    {
        // initialize the protected variables in the constructor.
        // These should be overriden in subclasses to initialize
        // appripriate values for other jetpack classes that inherit from this base class.
        step = SuperChickenCape.STEP;
        power = SuperChickenCape.POWER;
        fuel = SuperChickenCape.MAX_FUEL;
        fuelToAdd = SuperChickenCape.FUEL_ADD_FLIGHT;
        fuelToMinus = SuperChickenCape.FUEL_MINUS;
        fuelToAddInFlight = SuperChickenCape.FUEL_ADD_FLIGHT;
        maxFuel = SuperChickenCape.MAX_FUEL;
        numScrews = SuperChickenCape.NUM_SCREWS;
        numMetal = SuperChickenCape.NUM_Metal;
        specialPart = SuperChickenCape.SPECIAL_PART;
    }

    // Use this for initialization                            
    void Start()
    {


        this.jetNoise = this.GetComponentInChildren<AudioSource>();

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

    public virtual void JetpackStart()
    {
        if (this.fuel >= 0)
        {
            this.velocity = new Vector3(0, UP_POWER, 0);


            this.fuel = this.fuel - Time.deltaTime * this.fuelToMinus;

            if (!jetNoise.isPlaying)
            {
                jetNoise.Play();
            }
        }
        else
        {
            this.velocity = Vector3.zero;

            jetNoise.Stop();
        }
    }

    public virtual void JetpackStop()
    {
        if (this.fuel < this.maxFuel)
        {
            this.fuel = this.fuel + Time.deltaTime * fuelToAdd;

            jetNoise.Stop();
        }
        this.velocity = Vector3.zero;
    }
}
