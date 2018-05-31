using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    public Image image;
    public ChickenMovement chickenMovement;

    private Color originalColor;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        chickenMovement = player.GetComponent<ChickenMovement>();

        originalColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        {
            image.fillAmount = chickenMovement.JetPack.Fuel / chickenMovement.JetPack.MaxFuel;

            image.color = Color.Lerp(Color.red, originalColor, image.fillAmount);
        }

        //if (image.fillAmount <= 0.25f)
        //    image.color = Color.red;
        //else if (image.fillAmount <= 0.5f)
        //    image.color = new Color(255, 255, 0);
        //else
        //    image.color = originalColor;

    }
}
