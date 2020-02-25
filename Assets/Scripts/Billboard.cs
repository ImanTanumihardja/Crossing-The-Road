using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{

    public Image[] billBoards;

    private int  activeBillBoard = 0;

    private float timer = 0.0f;
    private float lastTime = 0.0f;
    private float waitingTime = 10.0f;
    private Vector3 start = new Vector3(-2.3f, 0 , 0);
    private Vector3 end = new Vector3(2.3f, 0, 0);


    // Start is called before the first frame update
    void Start()
    {

        int r = Random.Range(0, billBoards.Length);

        billBoards[r].rectTransform.anchoredPosition = Vector3.zero;
        billBoards[activeBillBoard].rectTransform.anchoredPosition = start;
        activeBillBoard = r;

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int num = Random.Range(0, 100);
        if (timer - lastTime >= waitingTime && num > 60)
        {
            switchImage();
            lastTime = timer;
        }
    }

    void switchImage()
    {
        int r = Random.Range(0, billBoards.Length);

        if(r != activeBillBoard)
        {

            StartCoroutine(moveBillBoards(5, billBoards[r], billBoards[activeBillBoard]));

            activeBillBoard = r;
        }
    }

    private IEnumerator moveBillBoards(float time, Image billBoard, Image activeBillBoard)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            billBoard.rectTransform.anchoredPosition = Vector3.Lerp(billBoard.rectTransform.anchoredPosition, Vector3.zero, Time.deltaTime);
            activeBillBoard.rectTransform.anchoredPosition = Vector3.Lerp(activeBillBoard.rectTransform.anchoredPosition, end, Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        activeBillBoard.rectTransform.anchoredPosition = start;
    }

}
