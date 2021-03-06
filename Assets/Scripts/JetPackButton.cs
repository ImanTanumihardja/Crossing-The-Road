﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;


public class JetPackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;
    public ChickenMovement chickenMovement;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isPressed)
        {
            chickenMovement.JetPack.JetpackStart();
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        chickenMovement.JetPack.Trigger(true);
        chickenMovement.setJetpackOn(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        chickenMovement.JetPack.Trigger(false);
        chickenMovement.setJetpackOn(false);
    }
}
