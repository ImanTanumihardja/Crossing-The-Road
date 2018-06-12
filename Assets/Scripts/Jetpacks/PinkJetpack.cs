﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkJetpack : JetPack
{
    private const float STEP = 35f;  // how far the chicken moves
    private const float SIDE_STEP = 35f;  // How far the chicken moves sideways
    private const float FUEL_ADD_FLIGHT = 10f;   // increment to fuel in flight
    private const float FUEL_ADD_BRIDGE = 50f;  // increment to fuel on bridge
    private const float FUEL_MINUS = 75f;  // decriment to fuel
    private const float MAX_FUEL = 150f; // max fuel level of jetpack

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
    public PinkJetpack()
    {
        // initialize the protected variables in the constructor.
        // These should be overriden in subclasses to initialize
        // appripriate values for other jetpack classes that inherit from this base class.
        step = PinkJetpack.STEP;
        sideStep = PinkJetpack.SIDE_STEP;
        fuel = PinkJetpack.MAX_FUEL;
        fuelToAdd = PinkJetpack.FUEL_ADD_FLIGHT;
        fuelToMinus = PinkJetpack.FUEL_MINUS;
        fuelToAddInFlight = PinkJetpack.FUEL_ADD_FLIGHT;
        fuelToAddOnBridge = PinkJetpack.FUEL_ADD_BRIDGE;
        maxFuel = PinkJetpack.MAX_FUEL;
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