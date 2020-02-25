using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour, IDragHandler,IPointerUpHandler, IPointerDownHandler {

    //[SerializeField] private float joystickVisualDistance = 50;

    private Image bgImg;
    private Image joystickImg;
    public Vector3 InputDirection { set; get; }
    public ChickenMovement chickenMovement;

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;

    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (bgImg.rectTransform,
               ped.position,
               ped.pressEventCamera,
               out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);


            float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputDirection = new Vector3(x, 0, y);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            joystickImg.rectTransform.anchoredPosition =
                new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3)
                , InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
        }
          
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void Update()
    {
        if(InputDirection != Vector3.zero)
        {
           chickenMovement.Move(InputDirection);
        }
        else
        {
           // chickenMovement.Stop();
        }
    }

}
