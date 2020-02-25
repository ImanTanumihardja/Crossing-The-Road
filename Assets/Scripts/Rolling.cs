using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rolling : MonoBehaviour {

    public Transform rollContainer;
    public AnimationCurve RollCurve;
    public GameObject target;
    public Text jetpackName;
    public Text rollingText;

    private PickJetPack pickJetPack;
    private Transform[] rolls;
    private const float ROLL_OFFSET = 300.0f;
    private bool isRolling = false;
    private float transition;
    private string giftName;
    private int jetpackNum;

    private string[] common = {"GreenJetPack", "YellowJetPack", "OrangeJetPack", "RedJetPack", "BlueJetPack", "WhiteJetPack", "PinkJetPack" };
    private string[] rare = { "BlackWhiteJetPack", "RainbowJetPack" };
    private string[] legendary = {"MachineGunJetPack",  "WingManJetPack", "SuperChickenCape"};

    private GameObject jetpackManager;
    private GameObject jp;

	// Use this for initialization
	void Start () {

       

        jetpackManager = GameObject.Find("JetPackManager");
        pickJetPack = jetpackManager.GetComponent<PickJetPack>();
        rolls = new Transform[rollContainer.childCount];
		for(int i = 0; i < rollContainer.childCount; i++)
        {
            rolls[i] = rollContainer.GetChild(i);
        }



    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.R)){
            chestClick();
        }
        if (isRolling)
        {
            Vector3 end = (-Vector3.right * ROLL_OFFSET) * (rolls.Length - 1);
            rollContainer.transform.localPosition = Vector3.Lerp(Vector3.right * ROLL_OFFSET, end, RollCurve.Evaluate(transition));
            transition += Time.deltaTime / 2.0f;
            if(transition > 1)
            {
                isRolling = false;
                switch (giftName)
                {  
                    case "Common":
                        jetpackNum = Random.Range(0, common.Length);
                        for (int i = 0; i <= pickJetPack.jetpackList.Count; i++)
                        {
                            if (pickJetPack.jetpackList[i].name == common[jetpackNum])
                            {
                                jp = Instantiate(pickJetPack.jetpackList[i]);
                                jp.SetActive(true);
                                jp.transform.position = target.transform.position;
                                jetpackName.text = pickJetPack.jetpackList[i].name.ToString();
                                break;
                            }
                            
                        }
                        pickJetPack.myJetpacks[common[jetpackNum]] = true;
                        Debug.Log("You got a Common");
                        break;
                    case "Rare":
                        jetpackNum = Random.Range(0, rare.Length);
                        for (int i = 0; i <= pickJetPack.jetpackList.Count; i++)
                        {
                            if (pickJetPack.jetpackList[i].name == rare[jetpackNum])
                            {
                                jp =  Instantiate(pickJetPack.jetpackList[i]);
                                jp.SetActive(true);
                                jp.transform.position = target.transform.position;
                                jetpackName.text = pickJetPack.jetpackList[i].name.ToString();
                                break;
                            }

                        }
                        pickJetPack.myJetpacks[rare[jetpackNum]] = true;
                        Debug.Log("You got a Rare");
                        break;
                    case "Legendary":
                        jetpackNum = Random.Range(0, legendary.Length);
                        for (int i = 0; i <= pickJetPack.jetpackList.Count; i++)
                        {
                            if (pickJetPack.jetpackList[i].name == legendary[jetpackNum])
                            {
                                jp = Instantiate(pickJetPack.jetpackList[i]);
                                jp.SetActive(true);
                                jp.transform.position = target.transform.position;
                                jetpackName.text = pickJetPack.jetpackList[i].name.ToString();
                                break;
                            }
                            
                        }
                        pickJetPack.myJetpacks[legendary[jetpackNum]] = true;
                        Debug.Log("You got a Legendary");
                        break;
                }

                pickJetPack.SaveMyJetpacks();
                SceneManager.LoadScene("Space");
            }
        }
	}

    public void chestClick()
    {
        if (!isRolling)
        {
            StartCoroutine(changeColor());
            transition = 0;
            isRolling = true;
            float offset = 0.0f;
            List<int> indexes = new List<int>();
            for (int i = 0; i < rolls.Length; i++)
            {
                indexes.Add(i);
            }
            for (int i = 0; i < rolls.Length; i++)
            {
                int index = indexes[UnityEngine.Random.Range(0, indexes.Count)];
                indexes.Remove(index);
                rolls[index].transform.localPosition = Vector2.right * offset;
                offset += ROLL_OFFSET;
                giftName = rolls[index].name;
            }
        }
           
    }

    private IEnumerator changeColor()
    {
        float elapsedTime = 0;

        while (elapsedTime < 10.0f)
        {
            rollingText.color = Random.ColorHSV();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }
        
}
