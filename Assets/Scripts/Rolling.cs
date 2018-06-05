using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rolling : MonoBehaviour {

    public Transform rollContainer;
    public AnimationCurve RollCurve;

    private PickJetPack pickJetPack;
    private Transform[] rolls;
    private const float ROLL_OFFSET = 300.0f;
    private bool isRolling;
    private float transition;
    private string giftName;

    private string[] common = {"GreenJetpack", "YellowJetpack" };
    private string[] rare = { "RedJetpack", "OrangeJetpack" };
    private string[] legendary = {"BlueJetpack"};

	// Use this for initialization
	void Start () {
        GameObject jetpackManager = GameObject.Find("JetPackManager");
        pickJetPack = jetpackManager.GetComponent<PickJetPack>();
        rolls = new Transform[rollContainer.childCount];
		for(int i = 0; i < rollContainer.childCount; i++)
        {
            rolls[i] = rollContainer.GetChild(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
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
                        pickJetPack.myJetpacks[common[Random.Range(0, common.Length)]] = true;
                        Debug.Log("You got a Common");
                        break;
                    case "Rare":
                        pickJetPack.myJetpacks[rare[Random.Range(0, rare.Length)]] = true;
                        Debug.Log("You got a Rare");
                        break;
                    case "Legendary":
                        pickJetPack.myJetpacks["BlueJetpack"] = true;
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
        transition = 0;
        isRolling = true;
        float offset = 0.0f;
        List<int> indexes = new List<int>();
        for(int i = 0; i < rolls.Length; i++){
            indexes.Add(i);
        }
        for(int i = 0; i < rolls.Length; i++)
        {
            int index = indexes[UnityEngine.Random.Range(0, indexes.Count)];
            indexes.Remove(index);
            rolls[index].transform.localPosition = Vector2.right * offset;
            offset += ROLL_OFFSET;
            giftName = rolls[index].name;
        }
    }
}
