using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour {

    private const float STEP            = 25f;  // how far the chicken moves
    private const float POWER           = 1.0f;  // power of jetpack
    private const float FUEL_ADD_FLIGHT = 20f;   // increment to fuel in flight
    private const float FUEL_MINUS      = 50;  // decriment to fuel
    private const float MAX_FUEL        = 100f; // max fuel level of jetpack

    private const float NUM_SCREWS       = 100F;
    private const float NUM_Metal        = 100F;
    private const string SPECIAL_PART     = "none";

    protected private const float UP_POWER        = 1.25f; // the power to go up

    protected float step;
    protected float power;
    protected float fuel;
    protected float fuelToAdd;
    protected float fuelToMinus;
    protected float fuelToAddInFlight;
    protected float maxFuel;

    protected float numScrews;
    protected float numMetal;
    protected string specialPart;

    protected Vector3 velocity = Vector3.zero;
    protected ParticleSystem jetFlameLeft;
    protected ParticleSystem jetFlameRight;
    protected AudioSource jetNoise;
    protected bool isJetpackOn = false;

    /// <summary>
    /// Constructor for the JetPack class.
    /// </summary>
    public JetPack() {
        // initialize the protected variables in the constructor.
        // These should be overriden in subclasses to initialize
        // appripriate values for other jetpack classes that inherit from this base class.
        step                = JetPack.STEP;
        power               = JetPack.POWER;
        fuel                = JetPack.MAX_FUEL;
        fuelToAdd           = JetPack.FUEL_ADD_FLIGHT;
        fuelToMinus         = JetPack.FUEL_MINUS;
        fuelToAddInFlight   = JetPack.FUEL_ADD_FLIGHT;
        maxFuel             = JetPack.MAX_FUEL;
        numScrews           = JetPack.NUM_SCREWS;
        numMetal            = JetPack.NUM_Metal;
        specialPart         = JetPack.SPECIAL_PART;

    }

    // Use this for initialization                            
    void Start () {
        ParticleSystem[] psArray = this.GetComponentsInChildren<ParticleSystem>();
        this.jetFlameRight = psArray[0];
        this.jetFlameLeft = psArray[1];

        this.jetNoise = this.GetComponent<AudioSource>();

        jetFlameLeft.Stop();
        jetFlameRight.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.isJetpackOn)
        {
            JetpackStart();
        }
        else
        {
            JetpackStop();
        }
    }

    /// <summary>
    /// Property for fuel in the jetpack.
    /// </summary>
    public virtual float Fuel
    {
        get { return this.fuel; }
        set { this.fuel = value; }
    }

    /// <summary>
    /// Property for maximum value of fuel in this jetpack.
    /// </summary>
    public virtual float MaxFuel
    {
        get { return this.maxFuel; }
    }

    /// <summary>
    /// Property for step value.
    /// </summary>
    public virtual float Step
    {
        get { return this.step; }
    }

    /// <summary>
    /// Property for side step value.
    /// </summary>
    public virtual float Power
    {
        get { return this.power; }
        set { this.power = value;  }
    }

    /// <summary>
    /// Property for jetpack velocity vector.
    /// </summary>
    public virtual Vector3 Velocity
    {
        get { return this.velocity;  }
    }

    /// <summary>
    /// Property for whether the jetpack is on or off.
    /// </summary>
    public virtual bool IsJetpackOn
    {
        get { return this.isJetpackOn;  }
    }

    /// <summary>
    /// Property for Number of Screws.
    /// </summary>
    public virtual float Screws
    {
        get { return this.numScrews; }
    }

    /// <summary>
    /// Property for number of metal.
    /// </summary>
    public virtual float Metal
    {
        get { return this.numMetal; }
    }

    /// <summary>
    /// Property for special part.
    /// </summary>
    public virtual string SpecialPart
    {
        get { return this.specialPart; }
    }

    /// <summary>
    /// Method to trigger the jetpack on or off.
    /// </summary>
    /// <param name="isOn">Bool value of whether jetpack is triggered (true) or not (false).</param>
    public virtual void Trigger(bool isOn)
    {
        this.isJetpackOn = isOn;
    }

    public void JetpackFull()
    {
        fuel = maxFuel;
    }

    /// <summary>
    /// Method for jetpack to start going.
    /// </summary>
    public virtual void JetpackStart()
    {
        if (this.fuel > 0)
        {
            this.velocity = new Vector3(0, UP_POWER, 0);


            this.fuel = this.fuel - Time.deltaTime * this.fuelToMinus;
            if (!jetFlameLeft.isEmitting) jetFlameLeft.Play();
            if (!jetFlameRight.isEmitting) jetFlameRight.Play();
            if (!jetNoise.isPlaying)
            {
                jetNoise.Play();
            }
        }
        else
        {
            this.velocity = Vector3.zero;
            jetFlameLeft.Stop();
            jetFlameRight.Stop();
            jetNoise.Stop();
       }
    }

    /// <summary>
    /// Method for jetpack to stop going.
    /// </summary>
    public virtual void JetpackStop()
    {
        if (this.fuel < this.maxFuel)
        {
            this.fuel = this.fuel + Time.deltaTime * fuelToAdd;
            jetFlameLeft.Stop();
            jetFlameRight.Stop();
            jetNoise.Stop();
        }
        this.velocity = Vector3.zero;
    }


}
