using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour {

    private const float STEP            = 10f;  // how far the chicken moves
    private const float SIDE_STEP       = 10f;  // How far the chicken moves sideways
    private const float FUEL_ADD_FLIGHT = 5f;   // increment to fuel in flight
    private const float FUEL_ADD_BRIDGE = 50f;  // increment to fuel on bridge
    private const float FUEL_MINUS      = 65f;  // decriment to fuel
    private const float MAX_FUEL        = 100f; // max fuel level of jetpack

    protected float step;
    protected float sideStep;
    protected float fuel;
    protected float fuelToAdd;
    protected float fuelToMinus;
    protected float fuelToAddInFlight;
    protected float fuelToAddOnBridge;
    protected float maxFuel;

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
        sideStep            = JetPack.SIDE_STEP;
        fuel                = JetPack.MAX_FUEL;
        fuelToAdd           = JetPack.FUEL_ADD_FLIGHT;
        fuelToMinus         = JetPack.FUEL_MINUS;
        fuelToAddInFlight   = JetPack.FUEL_ADD_FLIGHT;
        fuelToAddOnBridge   = JetPack.FUEL_ADD_BRIDGE;
        maxFuel             = JetPack.MAX_FUEL;
    }

    // Use this for initialization                            
    void Start () {
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
        set { this.fuel = Mathf.Min(0, Mathf.Min(this.maxFuel, value)); }
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
    public virtual float SideStep
    {
        get { return this.sideStep; }
        set { this.sideStep = value;  }
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
    /// Method to trigger the jetpack on or off.
    /// </summary>
    /// <param name="isOn">Bool value of whether jetpack is triggered (true) or not (false).</param>
    public virtual void Trigger(bool isOn)
    {
        this.isJetpackOn = isOn;     
    }

    /// <summary>
    /// Method for jetpack to start going.
    /// </summary>
    public virtual void JetpackStart()
    {
        if (this.fuel >= 0)
        {
            this.velocity = new Vector3(0, 10, 0);
            this.fuel = this.fuel - Time.deltaTime * this.fuelToMinus;
            if (!jetFlameLeft.isEmitting) jetFlameLeft.Play();
            if (!jetFlameRight.isEmitting) jetFlameRight.Play();
            jetNoise.UnPause();
        }
        else
        {
            this.velocity = Vector3.zero;
            jetFlameLeft.Stop();
            jetFlameRight.Stop();
            jetNoise.Pause();
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
            jetNoise.Pause();
        }
        this.velocity = Vector3.zero;
    }

    /// <summary>
    /// Method for jetpack to refuel.
    /// </summary>
    /// <param name="isRefuel">Bool value of whether jetpack is refueling (true) or not (false).</param>
    public virtual void Refuel(bool isRefuel)
    {
        if (isRefuel)
        {
            this.fuelToAdd = this.fuelToAddOnBridge;
        }else
        {
            this.fuelToAdd = this.fuelToAddInFlight;
        }
    }
}
