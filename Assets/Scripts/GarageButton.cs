using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageButton : MonoBehaviour {

    public Canvas Garage;

    public void OpenGarage()
    {
        Garage.enabled = true;
        this.enabled = false;
    }
}
