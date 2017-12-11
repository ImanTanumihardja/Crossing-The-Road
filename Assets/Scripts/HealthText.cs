using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private Text text;

    public Health health;

    private string originalValue;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();

        originalValue = text.text;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = originalValue + health.fuel;
    }
}
