using System.Collections;
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
            chickenMovement.jetPack.JetpackStart();
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        chickenMovement.jetPack.Trigger(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        chickenMovement.jetPack.Trigger(false);
    }
}
